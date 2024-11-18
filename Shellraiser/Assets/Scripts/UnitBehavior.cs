using System.Collections;
using UnityEngine;

public class UnitBehavior : MonoBehaviour
{
    [SerializeField] private UnitBehaviorSO unitOrdinary;
    private Transform target; 
    private float attackTimer;
    private int currentHealth;

    void Start()
    {
        currentHealth = unitOrdinary.health;
    }

    void Update()
    {
        if (target == null)
        {
            // Jednostka porusza sie do przodu, jesli nie ma celu
            transform.position += Vector3.right * unitOrdinary.moveSpeed * Time.deltaTime;
        }
        else
        {
            // Jednostka zaczyna atakowac, jesli ma cel
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f)
            {
                Attack();
                AttackHome();
                attackTimer = unitOrdinary.attackCooldown;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Sprawdzenie, czy napotkano cel
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("HomeEnemy"))
        {
            target = collision.transform;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Jesli wrog wyjdzie z zasiêgu
        if (collision.transform == target)
        {
            target = null;
        }
    }

    void Attack()
    {
        if (target != null)
        {
            // Zadaje obrazenia wrogowi
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
            // Zadaje obrazenia wrogowi
            HomeHealth homeHealth = target.GetComponent<HomeHealth>();
            if (homeHealth != null)
            {
                homeHealth.TakeDamage(unitOrdinary.damage);
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

