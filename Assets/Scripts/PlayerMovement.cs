using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Character movement speed
    public Transform firePoint; // Reference to the fire point
    public Transform bubbleSpawnPosition; // Reference to the fire point

    private Rigidbody2D rb;
    private Vector2 movement;
    protected float maxSpeed = 5f;
    public Vector2 velocity;
    protected float decelerateSpeed = 0.005f;
    private bool facingRight = true; // Track the current facing direction of the player
    private float buoyancy = 0.25f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input from the player
        movement.x = Input.GetAxisRaw("Horizontal") * moveSpeed;
        movement.y = Input.GetAxisRaw("Vertical") * moveSpeed;

        // Flip the player based on the direction of movement
        if (movement.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (movement.x < 0 && facingRight)
        {
            Flip();
        }
        
        if (!Input.anyKeyDown)
        {
            Decelerate();
        }
        rb.velocity = velocity;
    }

    void FixedUpdate()
    {
        UpdateVelocity(movement);
        AddBuoyancy();
        // Move the character
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    void UpdateVelocity(Vector2 acceleration)
    {
        velocity += acceleration;
        velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
        velocity.y = Mathf.Clamp(velocity.y, -maxSpeed, maxSpeed);
    }

    public void Decelerate()
    {
        if (velocity.x < 0) velocity.x += decelerateSpeed;
        if (velocity.x > 0) velocity.x -= decelerateSpeed;
        if (velocity.y < 0) velocity.y += decelerateSpeed;
        if (velocity.y > 0) velocity.y -= decelerateSpeed;
        if (velocity.x <= 0.2f && velocity.x >= -0.2f)
            velocity.x = 0;
        if (velocity.y <= 0.2f && velocity.y >= -0.2f)
            velocity.y = 0;
    }

    void AddBuoyancy()
    {
        velocity.y += buoyancy;
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