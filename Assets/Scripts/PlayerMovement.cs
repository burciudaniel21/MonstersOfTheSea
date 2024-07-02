using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Character movement speed
    public Transform firePoint; // Reference to the fire point
    public Transform bubbleSpawnPosition; // Reference to the fire point

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool facingRight = true; // Track the current facing direction of the player

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input from the player
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Flip the player based on the direction of movement
        if (movement.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (movement.x < 0 && facingRight)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        // Move the character
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void Flip()
    {
        // Switch the way the player is facing
        facingRight = !facingRight;

        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;

        // Adjust the fire point's position
        Vector3 firePointPosition = firePoint.localPosition;
        firePointPosition.x *= -1;
        firePoint.localPosition = firePointPosition;

        // Manually flip the rotation of the fire point
        firePoint.localRotation = Quaternion.Euler(0f, facingRight ? 0f : 180f, 0f);


        // Adjust the bubbleSpawnPosition point's position
        Vector3 bubbleSpawn = bubbleSpawnPosition.localPosition;
        bubbleSpawn.x *= -1;
        bubbleSpawnPosition.localPosition = bubbleSpawn;

        // Manually flip the rotation of the bubbleSpawnPosition point
        bubbleSpawnPosition.localRotation = Quaternion.Euler(0f, facingRight ? 0f : 180f, 0f);

        // Repeat for other child components if necessary
        foreach (Transform child in transform)
        {
            Vector3 childPosition = child.localPosition;
            childPosition.x *= -1;
            child.localPosition = childPosition;
        }
    }

    public bool IsFacingRight()
    {
        return facingRight;
    }
}