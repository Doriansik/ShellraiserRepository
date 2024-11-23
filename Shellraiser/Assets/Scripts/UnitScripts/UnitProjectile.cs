using UnityEngine;

public class UnitProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 20f; // Prêdkoœæ pocisku
    [SerializeField] private int damage = 10; // Obra¿enia zadawane przez pocisk
    private Transform targetEnemy;

    public void Seek(Transform _target)
    {
        targetEnemy = _target;
    }

    void Update()
    {
        // Jeœli nie ma celu, zniszcz pocisk
        if (targetEnemy == null)
        {
            Destroy(gameObject);
            return;
        }

        // Oblicz kierunek do celu
        Vector3 direction = (targetEnemy.position - transform.position).normalized;

        // Oblicz odleg³oœæ do przejœcia w tej klatce
        float distanceThisFrame = speed * Time.deltaTime;

        // Jeœli pocisk osi¹gnie cel
        if (Vector3.Distance(transform.position, targetEnemy.position) <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        // Przesuñ pocisk w kierunku celu
        transform.position += direction * distanceThisFrame;

        // Obróæ pocisk w kierunku celu
        transform.LookAt(targetEnemy);
    }

    

    void HitTarget()
    {
        if (targetEnemy != null) // Upewnij siê, ¿e cel istnieje
        {
            EnemyBehavior enemy = targetEnemy.GetComponent<EnemyBehavior>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Zadaj obra¿enia
            }
        }

        Destroy(gameObject); // Zniszcz pocisk po trafieniu
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy")) 
        {
            HitTarget();
        }
    }
}
