using UnityEngine;

public class HomeHealth : MonoBehaviour
{ 
    [SerializeField] private int homeHealth;
    [SerializeField] private GameObject gameOverLay;
    [SerializeField] private GameObject storageButtons;
    private int currentHealth;


    public HealthBar healthBar;

    private void Start()
    {
        currentHealth = homeHealth;
        healthBar.SetMaxHealth(homeHealth);
    }

    void Update() // just for testing!!! ! ! !
    {
        if (Input.GetKeyDown(KeyCode.Q)) // press space to take 20 dmg for testing the hp bar
        {                                       
            TakeDamage(20);
        }
        
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthBar.SetHealth(currentHealth); //updating the health bar
        if (currentHealth <= 0)
        {
            Die();
        }
        Debug.Log(currentHealth);
    }


    private void Die()
    {
        Destroy(gameObject);
        gameOverLay.SetActive(true);
        storageButtons.SetActive(false);
    }
}
