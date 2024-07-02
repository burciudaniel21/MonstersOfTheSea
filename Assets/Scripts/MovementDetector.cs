using UnityEngine;
using System.Collections;

public class MovementDetector : MonoBehaviour
{
    public Transform prefabContainer; // The Transform to be used for prefab instantiation
    public GameObject prefab; // The prefab to be instantiated
    public float movementThreshold = 0.1f; // Minimum movement to trigger instantiation
    public float timeInterval = 1.0f; // Time interval between checks
    public float despawnTime = 0.5f; // Time before prefabs are despawned
    public float scatterRadius = 1.0f; // Radius for the base of the cone
    public float coneAngle = 30.0f; // Angle of the cone in degrees

    private Vector3 previousPosition;
    private float timer;

    private void Start()
    {
        // Initialize previous position to the current position of the object
        previousPosition = transform.position;
        timer = timeInterval;
    }

    private void Update()
    {
        // Update the timer
        timer -= Time.deltaTime;

        // Check if the timer has elapsed
        if (timer <= 0)
        {
            // Check if the object has moved beyond the threshold
            if (Vector3.Distance(transform.position, previousPosition) > movementThreshold)
            {
                // Instantiate a random number of prefabs
                InstantiateRandomPrefabs();

                // Update the previous position
                previousPosition = transform.position;
            }

            // Reset the timer
            timer = timeInterval;
        }
    }

    private void InstantiateRandomPrefabs()
    {
        // Generate a random number between 1 and 5 (inclusive)
        int numberOfPrefabs = Random.Range(1, 6);

        for (int i = 0; i < numberOfPrefabs; i++)
        {
            // Calculate a random position within the cone
            Vector3 randomPosition = GetRandomPositionInCone(prefabContainer.position, prefabContainer.forward, scatterRadius, coneAngle);

            // Instantiate the prefab at the calculated random position and the prefabContainer's rotation
            GameObject instantiatedPrefab = Instantiate(prefab, randomPosition, prefabContainer.rotation);

            // Start a coroutine to despawn the prefab after despawnTime seconds
            StartCoroutine(DespawningCoroutine(instantiatedPrefab));
        }
    }

    private Vector3 GetRandomPositionInCone(Vector3 origin, Vector3 direction, float radius, float angle)
    {
        // Convert the cone angle from degrees to radians
        float angleRad = angle * Mathf.Deg2Rad;

        // Generate a random point within the cone's base radius
        Vector3 randomPosition = Random.insideUnitCircle * radius;
        randomPosition.z = Mathf.Sqrt(radius * radius - randomPosition.x * randomPosition.x - randomPosition.y * randomPosition.y);

        // Apply the direction to the random position
        randomPosition = Quaternion.LookRotation(direction) * randomPosition;

        // Calculate a random point in the cone
        Vector3 conePosition = origin + randomPosition;

        return conePosition;
    }

    private IEnumerator DespawningCoroutine(GameObject prefab)
    {
        // Wait for despawnTime seconds
        yield return new WaitForSeconds(despawnTime);

        // Destroy the prefab
        Destroy(prefab);
    }
}
