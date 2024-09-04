using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterData
{
    public string ImgName;
    public int MaxHealth;
    public string Name;
}

public class CharacterManager : MonoBehaviour
{
    public Image CharacterImage;

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
           

            // Update text component of the Health bar
            healthSlider.GetComponentInChildren<Text>().text = "-/-";

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
            float tempHealth = Mathf.Lerp(healthSlider.value, Health, Time.deltaTime * lerpSpeed);
            healthSlider.value = tempHealth;
            healthSlider.GetComponentInChildren<Text>().text = (int)tempHealth + "/" + MaxHealth;


            // Update the color of the Fill area based on the health value
            UpdateFillColor();
        } 
    }

    public void LoadCharacterData(CharacterData characterData)
    {
        MaxHealth = characterData.MaxHealth;
        // always start full life
        Health = characterData.MaxHealth;
        healthSlider = GetComponentInChildren<Slider>();

        Sprite sprite = Resources.Load<Sprite>("images/" + characterData.ImgName);
        if(sprite == null)
        {
            Debug.LogError("Sprite not found in Resources folder: " + characterData.ImgName);
            return;
        }
        CharacterImage.sprite = sprite;

        if (healthSlider != null)
        {
            healthSlider.maxValue = MaxHealth;
            healthSlider.value = Health;
            healthSlider.GetComponentInChildren<Text>().text = "-/-";
            
            fillImage = healthSlider.fillRect.GetComponent<Image>();     
            if (fillImage != null)
            {
                UpdateFillColor();
            }
            else
            {
                Debug.LogError("No Image component found in the Fill area.");
            }
        }
    }

    /// <summary>
    /// Set the color of the Fill area based on the health value
    /// </summary>
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
