using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform firePoint; // Point from where the bullets will be fired
    public GameObject bulletPrefab; // Prefab of the bullet
    public float fireRate = 0.5f; // Time in seconds between each shot

    private float nextFireTime = 0f;
    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // Set the bullet's velocity based on the player's facing direction
        if (playerMovement.IsFacingRight())
        {
            rb.velocity = firePoint.right * bullet.GetComponent<Bullet>().speed;
        }
        else
        {
            rb.velocity = -firePoint.right * bullet.GetComponent<Bullet>().speed;
        }
    }
}