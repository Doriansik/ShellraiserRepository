using UnityEngine;

public class HomeEnemyHealth : MonoBehaviour
{ 
    [SerializeField] private int homeHealthEnemy;
    [SerializeField] private GameObject winOverlay;
    private int currentHealth;
    private float target;

    public EnemyHPBar healthBarEnemy;

    private void Start()
    {
        currentHealth = homeHealthEnemy;
        healthBarEnemy.SetMaxHealth(homeHealthEnemy);
    }

    void Update() // just for testing!!!!!
    {
        if (Input.GetKeyDown(KeyCode.Space)) // press space to take 20 dmg for testing the hp bar
        {                                       
            TakeDamage(20);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthBarEnemy.SetHealth(currentHealth); //updating the health bar
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        Debug.Log(currentHealth);
    }


    private void Die()
    {
        Destroy(gameObject);
        Time.timeScale = 0;
        winOverlay.SetActive(true);
    }
}
