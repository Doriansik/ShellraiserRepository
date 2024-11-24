using System.Collections;
using UnityEngine;

public class UnitBehavior : MonoBehaviour
{
    [SerializeField] private UnitBehaviorSO unitOrdinary;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject projectilePrefab; // Prefab pocisku dla jednostek dystansowych
    [SerializeField] private Transform firePoint; // Punkt wystrza³u pocisku
    [SerializeField] private float detectionRange = 10f; // Zasiêg wykrywania wrogów

    private Transform target;
    private Transform targetUnit;
    private float attackTimer;
    private int currentHealth;

    void Start()
    {
        currentHealth = unitOrdinary.health;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (unitOrdinary.isRanged)
        {
            FindTarget(); // Szukaj celu, jeœli jednostka dystansowa
        }

        if (target == null)
        {
            // Brak celu - jednostka siê porusza
            animator.SetBool("isAttacking", false);
            transform.position += Vector3.right * unitOrdinary.moveSpeed * Time.deltaTime;
        }
        
        if(target != null)
        {
            // Jednostka ma cel
            animator.SetBool("isAttacking", true);

            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f)
            {
                if (unitOrdinary.isRanged)
                {
                    RangedAttack(); // Atak dystansowy
                }
                else
                {
                    Attack();       // Atak wrêcz
                    AttackHome();   // Atak na HomeEnemy
                }
                attackTimer = unitOrdinary.attackCooldown;
            }
        }
    }

    void FindTarget()
    {
        // Wyszukaj najbli¿szego wroga w okreœlonym zasiêgu
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRange);
        float closestDistance = detectionRange;
        Transform closestTarget = null;

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy") || hit.CompareTag("HomeEnemy"))
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = hit.transform;
                }
            }
        }

        target = closestTarget; // Ustaw najbli¿szy cel jako target
    }

    void OnCollisionEnter(Collision collision)
    {
        // Sprawdzenie, czy napotkano cel
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("HomeEnemy"))
        {
            target = collision.transform;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            targetUnit = collision.transform;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Jeœli jednostka wyjdzie z zasiêgu celu
        if (collision.transform == target)
        {
            target = null;
        }

        if (collision.transform == targetUnit)
        {
            targetUnit = null;
        }

        // Jeœli nie ma ju¿ celu, jednostka powinna przejœæ do chodzenia
        if (target == null && targetUnit == null)
        {
            animator.SetBool("isAttacking", false);  // Wy³¹czenie animacji ataku
        }
    }
    void Attack()
    {
        if (target != null)
        {
            animator.SetBool("isAttacking", true);
            // Zadaje obra¿enia wrogowi
            EnemyBehavior enemy = target.GetComponent<EnemyBehavior>();
            if (enemy != null)
            {
                enemy.TakeDamage(unitOrdinary.damage);
            }
        }
    }

    void AttackHome()
    {
        if (target != null)
        {
            animator.SetBool("isAttacking", true);
            // Zadaje obra¿enia HomeEnemy
            HomeEnemyHealth homeEnemyHealth = target.GetComponent<HomeEnemyHealth>();
            if (homeEnemyHealth != null)
            {
                homeEnemyHealth.TakeDamage(unitOrdinary.damage);
            }
        }
    }

    void RangedAttack()
    {
        if (target != null)
        {
            // Wystrzeliwanie pocisku
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            ProjectileBehavior projectileScript = projectile.GetComponent<ProjectileBehavior>();
            if (projectileScript != null)
            {
                projectileScript.Initialize(target, unitOrdinary.damage); // Przekazanie celu i obra¿eñ
            }
        }
    }

    

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("isDead", true);
        StartCoroutine(DestroyAfterAnimation());
    }

    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }


}

