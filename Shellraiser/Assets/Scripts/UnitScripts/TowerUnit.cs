using System;
using System.Collections;
using UnityEngine;

public class TowerUnit : MonoBehaviour
{
    [SerializeField] private GameObject homeBase;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform target; // Cel do namierzania
    [SerializeField] private float range = 10f; // Zasiêg wie¿yczki
    [SerializeField] private GameObject projectilePrefab; // Prefab pocisku
    [SerializeField] private Transform firePoint; // Punkt, z którego strzela wie¿yczka
    [SerializeField] private float fireRate = 1f; // Czêstotliwoœæ strza³ów (1 strza³ na sekundê)

    private float fireCooldown = 0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

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


    void Shoot()
    {
        
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        UnitProjectile projectileScript = projectile.GetComponent<UnitProjectile>();
        animator.SetBool("isShooting", true);

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

    private void Die()
    {
            if (homeBase == null)
            {
                animator.SetBool("isDead", true);
                StartCoroutine(StopGameAfterAnimation());
            }
    }
    private IEnumerator StopGameAfterAnimation()
    {
        // Poczekaj na czas trwania animacji
        yield return new WaitForSeconds(2f);

        // Zatrzymaj grê po zakoñczeniu animacji
        Time.timeScale = 0f;
    }
}

