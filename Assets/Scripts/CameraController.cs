using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float boundaryPadding = 0.1f; // Padding around the boundaries

    private Camera mainCamera;
    private float cameraHeight;
    private float cameraWidth;

    private void Start()
    {
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found. Make sure there is a camera in the scene tagged as 'MainCamera'.");
            return;
        }

        // Calculate the camera's height and width based on the aspect ratio
        cameraHeight = 2f * mainCamera.orthographicSize;
        cameraWidth = cameraHeight * mainCamera.aspect;
    }

    private void LateUpdate()
    {
        if (mainCamera == null) return;
        ConstrainObjectsToBoundaries();
    }

private void ConstrainObjectsToBoundaries()
{
    foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType<GameObject>())
    {
            if (obj.CompareTag("Player"))
            {
                Vector3 pos = obj.transform.position;

                // Constrain the position within the camera boundaries considering padding
                float leftBoundary = mainCamera.transform.position.x - cameraWidth / 2 + boundaryPadding;
                float rightBoundary = mainCamera.transform.position.x + cameraWidth / 2 - boundaryPadding;
                float topBoundary = mainCamera.transform.position.y - cameraHeight / 2 + boundaryPadding;
                float bottomBoundary = mainCamera.transform.position.y + cameraHeight / 2 - boundaryPadding;

                Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
                PlayerMovement playerMovement = obj.GetComponent<PlayerMovement>();
                SpriteRenderer spriteRenderer = obj.GetComponentInChildren<SpriteRenderer>();
                PlayerHealth playerHealth = obj.GetComponent<PlayerHealth>();

                if (playerHealth.currentHealth > 0)
                {
                    if (rb != null && playerMovement != null && spriteRenderer != null)
                    {
                        Bounds bounds = spriteRenderer.bounds;
                        Vector2 spriteSize = bounds.size / 2.0f; // Get half-size of the sprite

                        bool hitBoundary = false;

                        if (pos.x - spriteSize.x < leftBoundary)
                        {
                            Vector2 velocity = playerMovement.velocity;
                            velocity.x = -velocity.x;
                            playerMovement.velocity = velocity;

                            pos.x = leftBoundary + spriteSize.x; // Place the player back inside the left boundary
                            hitBoundary = true;
                        }
                        else if (pos.x + spriteSize.x > rightBoundary)
                        {
                            Vector2 velocity = playerMovement.velocity;
                            velocity.x = -velocity.x;
                            playerMovement.velocity = velocity;

                            pos.x = rightBoundary - spriteSize.x; // Place the player back inside the right boundary
                            hitBoundary = true;
                        }

                        if (pos.y - spriteSize.y < topBoundary)
                        {
                            Vector2 velocity = playerMovement.velocity;
                            velocity.y = -velocity.y;
                            playerMovement.velocity = velocity;

                            pos.y = topBoundary + spriteSize.y; // Place the player back inside the top boundary
                            hitBoundary = true;
                        }
                        else if (pos.y + spriteSize.y > bottomBoundary)
                        {
                            Vector2 velocity = playerMovement.velocity;
                            velocity.y = -velocity.y;
                            playerMovement.velocity = velocity;

                            pos.y = bottomBoundary - spriteSize.y; // Place the player back inside the bottom boundary
                            hitBoundary = true;
                        }

                        if (hitBoundary)
                        {
                            // Apply a nudge to the position based on the new velocity to prevent sticking
                            Vector3 nudge = new Vector3(playerMovement.velocity.x, playerMovement.velocity.y, 0).normalized * Time.deltaTime * Mathf.Max(Mathf.Abs(playerMovement.velocity.x), Mathf.Abs(playerMovement.velocity.y));
                            pos += nudge;
                            obj.transform.position = pos;
                        }
                    }

                }
            }
    }
}






}
