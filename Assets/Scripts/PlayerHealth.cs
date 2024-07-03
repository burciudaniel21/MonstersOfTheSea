using UnityEngine;
using UnityEngine.UI;  // Ensure you have this namespace for the Image component

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    public int maxHealth = 100; // Maximum HP of the player
    public Image healthBar; // Reference to the UI Image representing the health bar
    public AudioSource audioSource;
    public AudioClip[] metallicSounds;

    public int currentHealth;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        currentHealth = maxHealth;

        // Initialize the health bar
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
         if(metallicSounds != null && audioSource != null)
        {
            int randomSoundPicker = Random.Range(0, metallicSounds.Length - 1);
            audioSource.PlayOneShot(metallicSounds[randomSoundPicker]);
        }

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

    public float GetHealthPercentage()
    {
        return (float)currentHealth / (float)maxHealth;
    }

    public void Die()
    {
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerShooting>().enabled = false;
    }
}
