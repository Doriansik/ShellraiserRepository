using System.Collections;
using UnityEngine;

public class UnitBehavior : MonoBehaviour
{
    [SerializeField] private UnitBehaviorSO unitOrdinary;
    [SerializeField] private Animator animator;
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
        if (target == null && targetUnit == null)
        {
            animator.SetBool("isAttacking", false);
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
        
        if (collision.gameObject.CompareTag("Player"))
        {
            targetUnit = collision.transform;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Jesli wrog wyjdzie z zasiêgu
        if (collision.transform == target)
        {
            target = null;
        }
        
        if (collision.transform == targetUnit)
        {
            targetUnit = null;
        }
    }

    void Attack()
    {
        if (target != null)
        {
            animator.SetBool("isAttacking", true);
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
            animator.SetBool("isAttacking", true);
            // Zadaje obrazenia wrogowi
            HomeEnemyHealth homeEnemyHealth = target.GetComponent<HomeEnemyHealth>();
            if (homeEnemyHealth != null)
            {
                homeEnemyHealth.TakeDamage(unitOrdinary.damage);
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

        // Zatrzymaj grê po zakoñczeniu animacji
        Destroy(gameObject);
    }
}

