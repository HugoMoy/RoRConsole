using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public int Health;
    public int MaxHealth;
    private Slider healthSlider;
    private Image fillImage; 
    private float lerpSpeed = 5f;
    private int previousHealth;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Slider component from the child GameObject
        healthSlider = GetComponentInChildren<Slider>();

        if (healthSlider != null)
        {
            // Set the maxValue and initial value of the Slider
            healthSlider.maxValue = MaxHealth;
            healthSlider.value = Health;

            // Get the Image component of the Fill area
            fillImage = healthSlider.fillRect.GetComponent<Image>();

            if (fillImage != null)
            {
                // Set the color of the Fill area based on the health value
                UpdateFillColor();
            }
            else
            {
                Debug.LogError("No Image component found in the Fill area.");
            }
        }
        else
        {
            Debug.LogError("No Slider component found in children.");
        }
        
        // Initialize previousHealth
        previousHealth = Health;
    }

    // Update is called once per frame
    void Update()
    {
       if (healthSlider != null)
        {
            // Update the Slider value with the current Health
            healthSlider.value = Mathf.Lerp(healthSlider.value, Health, Time.deltaTime * lerpSpeed);

            // Update the color of the Fill area based on the health value
            UpdateFillColor();
        } 
    }

    private void UpdateFillColor()
    {
        if (fillImage != null)
        {
            // Calculate the health percentage
            float healthPercentage = (float)Health / MaxHealth;

            // Set the color to green when health is full and red when health is zero
            fillImage.color = Color.Lerp(Color.red, Color.green, healthPercentage);
        }
    }

    public void TakeDamage(int damage)
    {
        previousHealth = Health;
        Health -= damage;
        if (Health < 0)
        {
            Health = 0;
        }

        // Update the Slider value and color
        if (healthSlider != null)
        {
            UpdateFillColor();
        }
    }
}
