using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BubbleRise : MonoBehaviour
{
    public float riseSpeed = 1.0f; // Speed at which the bubble rises
    public float maxRiseTime = 3.0f; // Maximum time the bubble will rise before popping

    private float riseTimer;

    private void Start()
    {
        riseTimer = maxRiseTime;
    }

    private void Update()
    {
        // Move the bubble upwards
        transform.Translate(Vector3.up * riseSpeed * Time.deltaTime);

        // Decrease the rise timer
        riseTimer -= Time.deltaTime;

        // Destroy the bubble when the timer runs out
        if (riseTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
