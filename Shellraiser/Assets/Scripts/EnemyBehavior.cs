using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    [SerializeField] private EnemyBehaviorSO enemyOrdinary;    

    private Transform target;
    private float attackTimer;
    private int currentHealth;


    private void Start()
    {
        currentHealth = enemyOrdinary.health;
    }

    void Update()
    {
        if (target == null)
        {
            // Przeciwnik porusza sie do przodu, jesli nie ma celu
            transform.position += Vector3.left * enemyOrdinary.moveSpeed * Time.deltaTime;
        }
        else
        {
            // Przeciwnik zaczyna atakowac, jesli ma cel
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
    }

    void OnCollisionExit(Collision collision)
    {
        // Jesli jednostka wyjdzie z zasiegu
        if (collision.transform == target)
        {
            target = null;
        }
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
        Destroy(gameObject); // Niszczy jednostke
    }
}

