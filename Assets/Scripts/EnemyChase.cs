using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public float moveSpeed = 3f; // Enemy movement speed
    public int damage = 10; // Damage dealt to the player
    public float damageInterval = 1f; // Time in seconds between each damage application

    private Transform player; // Reference to the player's position
    private PlayerHealth playerHealth; // Reference to the player's health
    private bool isTouchingPlayer = false; // Is the enemy currently touching the player
    private float nextDamageTime = 0f; // Time when the next damage can be dealt
    private bool facingRight = true; // Is the enemy facing right

    void Start()
    {
        // Find the player by tag and get the PlayerHealth component
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            playerHealth = playerObject.GetComponent<PlayerHealth>();
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Calculate direction to the player
            Vector2 direction = (player.position - transform.position).normalized;

            // Move the enemy towards the player
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

            // Rotate the enemy to face the direction of movement
            if (direction.x > 0 && !facingRight)
            {
                Flip();
            }
            else if (direction.x < 0 && facingRight)
            {
                Flip();
            }
        }

        // Deal continuous damage if the enemy is touching the player
        if (isTouchingPlayer && Time.time >= nextDamageTime)
        {
            playerHealth.TakeDamage(damage);
            nextDamageTime = Time.time + damageInterval;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the enemy collides with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouchingPlayer = true;
            playerHealth.TakeDamage(damage);
            nextDamageTime = Time.time + damageInterval;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the enemy stops colliding with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouchingPlayer = false;
        }
    }

    void Flip()
    {
        // Switch the way the enemy is facing
        facingRight = !facingRight;

        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
