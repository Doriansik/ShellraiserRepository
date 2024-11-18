using UnityEngine;

public class HomeHealth : MonoBehaviour
{ 
    [SerializeField] private int homeHealth;
    private Transform target;
    private int currentHealth;

    private void Start()
    {
        currentHealth = homeHealth;
    }

    

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
        Debug.Log(currentHealth);
    }


    private void Die()
    {
        Destroy(gameObject);
    }
}
