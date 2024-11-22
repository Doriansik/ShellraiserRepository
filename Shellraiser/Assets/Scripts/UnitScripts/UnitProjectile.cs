using UnityEngine;

public class UnitProjectile : MonoBehaviour
{
    [SerializeField] private EnemyBehavior enemyBehavior;
    public float speed = 20f; // Prêdkoœæ pocisku
    public int damage = 10; // Obra¿enia zadawane przez pocisk
    private Transform target;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    void HitTarget()
    {
        enemyBehavior.TakeDamage(damage);
        Destroy(gameObject);
        // Jeœli masz skrypt na przeciwnika, mo¿esz tu dodaæ odbieranie obra¿eñ
    }
}
