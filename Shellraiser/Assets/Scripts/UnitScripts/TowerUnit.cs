using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target; // Cel do namierzania
    public float range = 10f; // Zasiêg wie¿yczki
    public float rotationSpeed = 5f; // Prêdkoœæ obracania wie¿yczki
    public GameObject projectilePrefab; // Prefab pocisku
    public Transform firePoint; // Punkt, z którego strzela wie¿yczka
    public float fireRate = 1f; // Czêstotliwoœæ strza³ów (1 strza³ na sekundê)

    private float fireCooldown = 0f;

    void Update()
    {
        // ZnajdŸ najbli¿szego przeciwnika w zasiêgu
        FindTarget();

        // Jeœli mamy cel
        if (target != null)
        {
            RotateTowardsTarget();

            // Strzelaj, jeœli cooldown pozwala
            if (fireCooldown <= 0f)
            {
                Shoot();
                fireCooldown = 1f / fireRate;
            }
        }

        // Odliczanie cooldownu
        fireCooldown -= Time.deltaTime;
    }

    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && distanceToEnemy <= range)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void RotateTowardsTarget()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction + new Vector3(0,270,0));
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        UnitProjectile projectileScript = projectile.GetComponent<UnitProjectile>();

        if (projectileScript != null)
        {
            projectileScript.Seek(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Wizualizacja zasiêgu w edytorze
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

