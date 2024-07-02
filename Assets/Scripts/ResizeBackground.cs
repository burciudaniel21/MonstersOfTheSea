using UnityEngine;

public class ResizeBackground : MonoBehaviour
{
    public Camera mainCamera;  // Reference to the main camera

    [Header("Scaling Offset")]
    public Vector2 scaleOffset = Vector2.one;  // Scale factor for X and Y, default to (1,1) which means no additional scaling

    private SpriteRenderer spriteRenderer;  // Reference to the SpriteRenderer component

    private void Awake()
    {
        // Automatically detect the main camera if none is assigned
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (mainCamera == null)
        {
            Debug.LogError("No camera found. Please assign a camera to the ResizeBackground script.");
        }

        // Cache the SpriteRenderer component
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer found in children. Please add a SpriteRenderer to the background prefab.");
        }
    }

    private void Start()
    {
        // Ensure the camera is set and SpriteRenderer is found before starting the resize process
        if (mainCamera != null && spriteRenderer != null)
        {
            ResizeToCamera();
        }
    }

    private void ResizeToCamera()
    {
        if (mainCamera == null || spriteRenderer == null)
        {
            Debug.LogError("Cannot resize background. Main Camera or SpriteRenderer is missing.");
            return;
        }

        // Get the sprite
        Sprite sprite = spriteRenderer.sprite;
        if (sprite == null)
        {
            Debug.LogError("The SpriteRenderer does not have a sprite assigned. Please assign a sprite.");
            return;
        }

        // Get the size of the sprite in world units
        Vector2 spriteSize = sprite.bounds.size;

        // Calculate the required size to cover the camera view
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // Calculate the scale factors for X and Y
        float scaleX = (cameraWidth / spriteSize.x) * scaleOffset.x;
        float scaleY = (cameraHeight / spriteSize.y) * scaleOffset.y;

        //// Log the calculated values for debugging
        //Debug.Log($"Camera Width: {cameraWidth}, Camera Height: {cameraHeight}");
        //Debug.Log($"Sprite Width: {spriteSize.x}, Sprite Height: {spriteSize.y}");
        //Debug.Log($"ScaleX: {scaleX}, ScaleY: {scaleY}");
        //Debug.Log($"ScaleOffset X: {scaleOffset.x}, Y: {scaleOffset.y}");

        // Apply the scale to the background
        transform.localScale = new Vector3(scaleX, scaleY, 1f);

        // Ensure that the background's position is set correctly to match the camera's center
        transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, transform.position.z);
    }

    // Optional: Call this method if you want to change the scaleOffset dynamically
    public void SetScaleOffset(Vector2 newScaleOffset)
    {
        scaleOffset = newScaleOffset;
        ResizeToCamera();  // Reapply the resizing with the new scale offset
    }

    // Optional: Call this method to manually trigger the resize, useful if camera or other settings change
    public void TriggerResize()
    {
        ResizeToCamera();
    }

    private void OnValidate()
    {
        // Only trigger resizing in the editor if the game is running
        if (Application.isPlaying)
        {
            ResizeToCamera();
        }
        else
        {
            // Ensure the camera is found
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }
        }
    }
}
