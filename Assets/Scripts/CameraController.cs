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
                pos.x = Mathf.Clamp(pos.x, mainCamera.transform.position.x - cameraWidth / 2 + boundaryPadding, mainCamera.transform.position.x + cameraWidth / 2 - boundaryPadding);
                pos.y = Mathf.Clamp(pos.y, mainCamera.transform.position.y - cameraHeight / 2 + boundaryPadding, mainCamera.transform.position.y + cameraHeight / 2 - boundaryPadding);

                obj.transform.position = pos;
            }
        }
    }


}
