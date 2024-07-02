using UnityEngine;
using UnityEngine.UI;  // Ensure you have this namespace for the Image component

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maximum HP of the player
    public Image healthBar; // Reference to the UI Image representing the health bar

    public int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;

        // Initialize the health bar
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            // Update the fill amount of the health bar image
            healthBar.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    public void Die()
    {
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerShooting>().enabled = false;
    }
}
