using UnityEngine;
using UnityEngine.UI; // Make sure to include this to use the Slider component

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50; // Maximum HP of the enemy
    private int currentHealth;

    public Slider healthBar; // Reference to the UI Slider component for the health bar

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (healthBar != null)
        {
            healthBar.value = currentHealth; // Update the health bar value
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
