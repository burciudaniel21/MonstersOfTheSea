using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTint : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Color normalColor = Color.white;
    public Color lowHealthColor = Color.red;
    public Color deadColor = Color.black;
    public float lowHealthThreshold = 0.3f;

    private PlayerHealth playerHealth;
    // Start is called before the first frame update
    void Start()
    {
         if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        playerHealth = PlayerHealth.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth != null)
        {
            float healthPercentage = playerHealth.GetHealthPercentage();
            Debug.Log(healthPercentage);
            if (healthPercentage <= lowHealthThreshold)
            {
                if (healthPercentage <= 0)
                { 
                    spriteRenderer.color = deadColor;
                }
                else
                {
                    spriteRenderer.color = lowHealthColor;
                }
            }
            else
            {
                spriteRenderer.color = normalColor;
            }
        }
    }
}
