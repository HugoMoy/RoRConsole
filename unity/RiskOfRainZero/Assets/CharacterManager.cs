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

    public Text TextEffectTemplate;
    // public string CharacterName;
    public int Health;
    public int MaxHealth;
    public Slider healthSlider;
    private Image healthfillImage; 
    private float lerpSpeed = 5f;
    private int previousHealth;

    // Start is called before the first frame update
    void Start()
    {

        
        if (healthSlider != null)
        {

            // Update text component of the Health bar
            healthSlider.GetComponentInChildren<Text>().text = "-/-";

            // Get the Image component of the Fill area
            healthfillImage = healthSlider.fillRect.GetComponent<Image>();
            

            if (healthfillImage != null)
            {
                // Set the color of the Fill area based on the health value
                UpdateHealthSlider();
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
            UpdateHealthSlider();
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
            
            healthfillImage = healthSlider.fillRect.GetComponent<Image>();     
            if (healthfillImage != null)
            {
                UpdateHealthSlider();
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
    private void UpdateHealthSlider()
    {
        if (healthfillImage != null)
        {
            // Calculate the health percentage
            float healthPercentage = (float)Health / MaxHealth;

            // Set the color to green when health is full and red when health is zero
            healthfillImage.color = Color.Lerp(Color.red, Color.green, healthPercentage);
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
        // Show a text effect that disappears after a few seconds
        ShowTextEffect("-" + damage, Color.red);

        // Update the Slider value and color
        if (healthSlider != null)
        {
            UpdateHealthSlider();
        }
    }

    public void ShowTextEffect(string text, Color color)
    {        
        Canvas canvas = this.GetComponentInChildren<Canvas>();
        GameObject textEffectInstance = Instantiate(TextEffectTemplate.gameObject, canvas.transform);
        textEffectInstance.SetActive(false);

        Text textComponent = textEffectInstance.GetComponent<Text>();
        if (textComponent != null)
        {
            textComponent.text = text;
            textComponent.fontSize = 25;
            textComponent.color = color;
            textComponent.alignment = TextAnchor.MiddleCenter; // Center the text
            if (textComponent.font == null)
            {
                textComponent.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            }
        }
        else
        {
            Debug.LogError("Text component not found on the instantiated TextEffectTemplate.");
            return;
        }
        
        textEffectInstance.transform.position = CharacterImage.transform.position;
        textEffectInstance.transform.SetAsLastSibling();

        TextMoveAndFadeHandler textMoveAndFadeHandler = textEffectInstance.GetComponent<TextMoveAndFadeHandler>();
        if (textMoveAndFadeHandler != null)
        {
            textMoveAndFadeHandler.Initialize(Vector3.up, 35, 2, 3); // Example parameters
        }
        else
        {
            Debug.LogError("No TextMoveAndFadeHandler component found on the instantiated TextEffectTemplate.");
        }

        textEffectInstance.SetActive(true);
    }
}
