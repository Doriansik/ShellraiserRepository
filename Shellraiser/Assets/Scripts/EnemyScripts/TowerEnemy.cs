using System.Collections;
using UnityEngine;

public class TowerEnemy : MonoBehaviour
{
    [SerializeField] private GameObject enemyBase;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform target; // Cel do namierzania
    [SerializeField] private float range = 10f; // Zasiêg wie¿yczki
    [SerializeField] private GameObject projectilePrefab; // Prefab pocisku
    [SerializeField] private Transform firePoint; // Punkt, z którego strzela wie¿yczka
    [SerializeField] private float fireRate; // Czêstotliwoœæ strza³ów (1 strza³ na sekundê)

    private float fireCooldown = 0f;

    void Update()
    {
        Die();
        // ZnajdŸ najbli¿szego przeciwnika w zasiêgu
        FindTarget();
        // Jeœli mamy cel
        if (target != null)
        {

            // Strzelaj, jeœli cooldown pozwala
            if (fireCooldown <= 0f)
            {

                Shoot();
                fireCooldown = 1f / fireRate;
            }
        }
        else
        {
            animator.SetBool("isShooting", false);
        }
        // Odliczanie cooldownu
        fireCooldown -= Time.deltaTime;
    }

    void FindTarget()
    {
        GameObject[] units = GameObject.FindGameObjectsWithTag("Player");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestUnit = null;

        foreach (GameObject unit in units)
        {
            float distanceToUnit = Vector3.Distance(transform.position, unit.transform.position);
            if (distanceToUnit < shortestDistance && distanceToUnit <= range)
            {
                shortestDistance = distanceToUnit;
                nearestUnit = unit;
            }
        }

        if (nearestUnit != null)
        {
            target = nearestUnit.transform;
        }
        else
        {
            target = null;
        }
    }
    void Shoot()
    {
        GameObject projectileEnemy = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        EnemyProjectile projectileScript = projectileEnemy.GetComponent<EnemyProjectile>();
        animator.SetBool("isShooting", true);

        if (projectileScript != null)
        {
            projectileScript.SeekUnit(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Wizualizacja zasiêgu w edytorze
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void Die()
    {
        if (enemyBase == null)
        {
            animator.SetBool("isDead", true);
            StartCoroutine(StopGameAfterAnimation());
        }
    }
    private IEnumerator StopGameAfterAnimation()
    {
        // Poczekaj na czas trwania animacji
        yield return new WaitForSeconds(3f);

        // Zatrzymaj grê po zakoñczeniu animacji
        Time.timeScale = 0f;
    }
}
