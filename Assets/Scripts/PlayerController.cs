using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;  // Ensure you have this namespace for TextMesh Pro

public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;  // Assign this in the Inspector with a TextMeshProUGUI element
    public PlayerHealth playerHealth;
    private EnemyHealth[] enemies;

    private void Start()
    {
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);  // Ensure the game over text is hidden at the start
        }
    }

    private void Update()
    {
        enemies = FindObjectsOfType<EnemyHealth>();

        if (enemies.Length == 0)
        {
            Victory();
        }

        if(playerHealth.currentHealth <= 0)
        {
            Die();
        }
        // Check if the player presses the "R" key to restart the game
        if (Input.GetKeyDown(KeyCode.R) && gameOverText.gameObject.activeSelf)
        {
            RestartGame();
        }
    }

    public void Die()
    {
        if (gameOverText != null)
        {
            gameOverText.text = "You have been defeated. Press \"R\" to restart.";
            gameOverText.gameObject.SetActive(true);  // Show the game over text
        }
    }
    public void Victory()
    {
        if (gameOverText != null)
        {
            gameOverText.text = "Congratulations! You win. Press \"R\" to restart.";
            gameOverText.gameObject.SetActive(true);  // Show the game over text
        }
    }

    private void RestartGame()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
