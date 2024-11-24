using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 20f; // Prêdkoœæ pocisku
    [SerializeField] private int damage = 10; // Obra¿enia zadawane przez pocisk
    private Transform targetUnit;

    public void SeekUnit(Transform _target)
    {
        targetUnit = _target;
    }

    void Update()
    {
        // Jeœli nie ma celu, zniszcz pocisk
        if (targetUnit == null)
        {
            Destroy(gameObject);
            return;
        }

        // Oblicz kierunek do celu
        Vector3 direction = (targetUnit.position - transform.position).normalized;

        // Oblicz odleg³oœæ do przejœcia w tej klatce
        float distanceThisFrame = speed * Time.deltaTime;

        // Jeœli pocisk osi¹gnie cel
        if (Vector3.Distance(transform.position, targetUnit.position) <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        // Przesuñ pocisk w kierunku celu
        transform.position += direction * distanceThisFrame;

        // Obróæ pocisk w kierunku celu
        transform.LookAt(targetUnit);
    }



    void HitTarget()
    {
        if (targetUnit != null) // Upewnij siê, ¿e cel istnieje
        {
            UnitBehavior unit = targetUnit.GetComponent<UnitBehavior>();
            if (unit != null)
            {
                unit.TakeDamage(damage); // Zadaj obra¿enia
            }
        }

        Destroy(gameObject); // Zniszcz pocisk po trafieniu
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HitTarget();
        }
    }
}
