using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 20f; // Prêdkoœæ pocisku
    public float damage = 10f; // Obra¿enia zadawane przez pocisk
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
        // Tu mo¿esz dodaæ efekty trafienia
        Destroy(gameObject);
        // Jeœli masz skrypt na przeciwnika, mo¿esz tu dodaæ odbieranie obra¿eñ
    }
}
