using System.Collections;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    [SerializeField] private EnemyBehaviorSO enemyOrdinary;
    //[SerializeField] private Animator animator;

    private Transform target;
    private Transform targetEnemy;
    private float attackTimer;
    private int currentHealth;


    private void Start()
    {
        currentHealth = enemyOrdinary.health;
        //animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (target == null && targetEnemy == null)
        {
            // Przeciwnik porusza siê do przodu, jeœli nie ma celu
            transform.position += Vector3.left * enemyOrdinary.moveSpeed * Time.deltaTime;
        }
        else
        {
            // Przeciwnik zaczyna atakowaæ, jeœli ma cel
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f)
            {
                Attack();
                AttackHome();
                attackTimer = enemyOrdinary.attackCooldown;
            }
        }
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
            // Je¿eli napotkano inn¹ jednostkê, sprawdzamy, czy mo¿na j¹ zablokowaæ
            targetEnemy = collision.transform;

            // Sprawdzamy, czy jednostka, która dotyka, jest blokuj¹c¹
            EnemyBehavior enemyBehavior = collision.gameObject.GetComponent<EnemyBehavior>();
            if (enemyBehavior != null)
            {
                // Jeœli jednostka ju¿ dotknê³a i nie ma wolnej drogi, to zatrzymujemy j¹
                if (IsBlocked())
                {
                    targetEnemy = null; // Przerywamy atakowanie
                }
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Jeœli jednostka wyjdzie z zasiêgu, przestaje byæ celem
        if (collision.transform == target)
        {
            target = null;
        }

        if (collision.transform == targetEnemy)
        {
            targetEnemy = null;
        }
    }

    bool IsBlocked()
    {
        // Sprawdzamy, czy jednostka nie ma wolnej drogi
        // Jeœli inne jednostki blokuj¹ j¹, powinna siê zatrzymaæ
        // Mo¿esz dodaæ tutaj prosty check dla kolizji z innymi jednostkami
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f); // Zasiêg sprawdzania kolizji
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                return true; // Jednostka jest zablokowana przez inn¹ jednostkê
            }
        }
        return false; // Jednostka ma woln¹ drogê
    }


    void Attack()
    {
        if (target != null)
        {
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
            // Zadaje obrazenia wrogowi
            HomeHealth homeHealth = target.GetComponent<HomeHealth>();
            if (homeHealth != null)
            {
                homeHealth.TakeDamage(enemyOrdinary.damage);
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
        Destroy(gameObject);
        //animator.SetBool("isDead", true);
        //StartCoroutine(DestroyAfterAnimation());
    }

    /*private IEnumerator DestroyAfterAnimation()
    {
        // Poczekaj na czas trwania animacji
        yield return new WaitForSeconds(2f);

        // Zatrzymaj grê po zakoñczeniu animacji
        Destroy(gameObject);
    }*/
}

