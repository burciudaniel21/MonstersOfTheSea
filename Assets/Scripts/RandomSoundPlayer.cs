using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
    public AudioSource audioSource;  // Reference to the AudioSource component
    public AudioClip audioClip;  // The sound clip to be played

    // Minimum and maximum intervals in seconds
    public float minInterval = 10f;
    public float maxInterval = 25f;

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (audioSource == null)
        {
            Debug.LogError("No AudioSource component found. Please add an AudioSource component to the GameObject.");
            return;
        }

        if (audioClip == null)
        {
            Debug.LogError("No AudioClip assigned. Please assign an AudioClip in the inspector.");
            return;
        }

        // Start the coroutine to play the sound at random intervals
        StartCoroutine(PlaySoundAtRandomIntervals());
    }

    private System.Collections.IEnumerator PlaySoundAtRandomIntervals()
    {
        while (true)
        {
            // Wait for a random time between minInterval and maxInterval
            float waitTime = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waitTime);

            // Play the sound clip
            audioSource.PlayOneShot(audioClip);
        }
    }
}
