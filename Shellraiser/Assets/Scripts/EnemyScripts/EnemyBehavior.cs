using System.Collections;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    [SerializeField] private EnemyBehaviorSO enemyOrdinary;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject projectilePrefab; // Prefab pocisku dla jednostek dystansowych
    [SerializeField] private Transform firePoint; // Punkt wystrza�u pocisku
    [SerializeField] private float detectionRange = 10f; // Zasi�g wykrywania wrog�w

    private Transform target;
    private Transform targetEnemy;
    private float attackTimer;
    private int currentHealth;



    private void Start()
    {
        currentHealth = enemyOrdinary.health;
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        if (enemyOrdinary.isRanged)
        {
            FindTarget(); // Szukaj celu, je�li jednostka dystansowa
        }

        if (target == null)
        {
            // Brak celu - jednostka si� porusza
            animator.SetBool("isAttacking", false);
            transform.position += Vector3.left * enemyOrdinary.moveSpeed * Time.deltaTime;
        }

        if (target != null)
        {
            // Jednostka ma cel
            animator.SetBool("isAttacking", true);

            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f)
            {
                if (enemyOrdinary.isRanged)
                {
                    RangedAttack(); // Atak dystansowy
                }
                else
                {
                    Attack();       // Atak wr�cz
                    AttackHome();   // Atak na HomeEnemy
                }
                attackTimer = enemyOrdinary.attackCooldown;
            }
        }

    }

    void FindTarget()
    {
        // Wyszukaj najbli�szego wroga w okre�lonym zasi�gu
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRange);
        float closestDistance = detectionRange;
        Transform closestTarget = null;

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Player") || hit.CompareTag("HomeUnit"))
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = hit.transform;
                }
            }
        }

        target = closestTarget; // Ustaw najbli�szy cel jako target
    }

    void OnCollisionEnter(Collision collision)
    {
        // Sprawdzenie, czy napotkano cel
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("HomeUnit"))
        {
            target = collision.transform;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Je�eli napotkano inn� jednostk�, sprawdzamy, czy mo�na j� zablokowa�
            targetEnemy = collision.transform;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Je�li jednostka wyjdzie z zasi�gu, przestaje by� celem
        if (collision.transform == target)
        {
            target = null;
        }

        if (collision.transform == targetEnemy)
        {
            targetEnemy = null;
        }

        if(target == null && targetEnemy == null)
        {
            animator.SetBool("isAttacking", false);
        }
    }


    void Attack()
    {
        if (target != null)
        {
            animator.SetBool("isAttacking", true);
            // Zadaje obrazenia jednostce
            UnitBehavior unit = target.GetComponent<UnitBehavior>();
            if (unit != null)
            {
                unit.TakeDamage(enemyOrdinary.damage);
            }
        }
    }

    void AttackHome()
    {
        if (target != null)
        {
            animator.SetBool("isAttacking", true);
            // Zadaje obrazenia wrogowi
            HomeHealth homeHealth = target.GetComponent<HomeHealth>();
            if (homeHealth != null)
            {
                homeHealth.TakeDamage(enemyOrdinary.damage);
            }
        }
    }

    void RangedAttack()
    {
        if (target != null)
        {
            // Wystrzeliwanie pocisku
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            EnemyProjectileBehavior projectileEnemyScript = projectile.GetComponent<EnemyProjectileBehavior>();
            if (projectileEnemyScript != null)
            {
                projectileEnemyScript.Initialize(target, enemyOrdinary.damage); // Przekazanie celu i obra�e�
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
        // Poczekaj na czas trwania animacji
        yield return new WaitForSeconds(2f);

        // Zatrzymaj gr� po zako�czeniu animacji
        Destroy(gameObject);
    }
}

