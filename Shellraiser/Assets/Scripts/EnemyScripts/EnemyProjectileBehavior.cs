using UnityEngine;

public class EnemyProjectileBehavior : MonoBehaviour
{
    private Transform target;
    private int damage;
    [SerializeField] private float speed = 10f;

    public void Initialize(Transform target, int damage)
    {
        this.target = target;
        this.damage = damage;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject); // Zniszcz pocisk, jeœli cel nie istnieje
            return;
        }

        // Przesuwanie pocisku w stronê celu
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Sprawdzenie, czy dotar³ do celu
        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            HitTarget();
        }
    }

    void HitTarget()
    {
        UnitBehavior unit = target.GetComponent<UnitBehavior>();
        if (unit != null)
        {
            unit.TakeDamage(damage);
        }
        Destroy(gameObject); // Zniszczenie pocisku po trafieniu
    }
}
