using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CharacterData
{
    public string ImgName;
    public int MaxHealth;
    public string Name;
    public int Strength;
    public int CritChance;
}

public struct DamageLine{
    public int Damage;
    public bool IsCritical;
    public List<string> AdditionalEffects;
}

public class Item
{
    public string Name;
    public string ImgName;
    public int Quantity;
}

public class CharacterManager : MonoBehaviour
{
    public Image CharacterImage;
    public Text TextEffectTemplate;
    public GameObject ItemTemplate;
    // public string CharacterName;
    public string EquipmentName;
    public int EquipmentCooldown;
    public int EquipmentCurrentCooldown;
    // Stats 
    public int Health;
    public int MaxHealth;
    public int Strength;
    public int CritChance;
    // UI
    public Slider healthSlider;
    private Image healthfillImage; 
    private float lerpSpeed = 5f;
    private int previousHealth;

    private List<Item> items = new List<Item>();

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

    public void AddItem(Item item)
    {
        if(items.Any(i => i.Name == item.Name))
        {
            items.First(i => i.Name == item.Name).Quantity += item.Quantity;
            // Update quantity in UI
            Transform itemHolder = this.transform.Find("Canvas/ItemHolder");
            if(itemHolder != null)
            {
                Transform itemTemplate = itemHolder.Find(item.Name);
                if(itemTemplate != null)
                {
                    itemTemplate.GetComponentInChildren<Text>().text = items.First(i => i.Name == item.Name).Quantity.ToString();                    
                }
                else
                {
                    Debug.LogError("ItemTemplate" + item.Name + " not found in children.");
                }
            }
            else
            {
                Debug.LogError("ItemHolder not found in children.");
            }
        }
        else
        {
            items.Add(item);
            // add itemTemplate in UI
            Transform itemHolder = this.transform.Find("Canvas/ItemHolder");
            if(itemHolder != null)
            {

                GameObject itemInstance = Instantiate(ItemTemplate.gameObject, itemHolder.transform);
                itemInstance.SetActive(false);

                Image imageComponent = itemInstance.GetComponentInChildren<Image>();
                Sprite sprite = Resources.Load<Sprite>("images/" + item.ImgName);
                if(sprite == null)
                {
                    Debug.LogError("Sprite not found in Resources folder: " + item.ImgName);
                    return;
                }

                itemInstance.GetComponentInChildren<Image>().sprite = sprite;
                itemInstance.name = item.Name;
                itemInstance.GetComponentInChildren<Text>().text = item.Quantity.ToString();
                itemInstance.SetActive(true);

            }
            else
            {
                Debug.LogError("ItemHolder not found in children.");
            }
        }        
    }

    public void LoadCharacterData(CharacterData characterData)
    {
        Strength = characterData.Strength;
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

    public List<DamageLine> ComputeDamage(int strPercentage, int iterations)
    {
        List<DamageLine> damages = new List<DamageLine>();
        for(int i = 0; i < iterations; i++)
        {
            List<string> additionalEffects = new List<string>();
            int damageDone = (Strength * strPercentage) / 100;            

            bool IsCritical = Random.Range(1, 101) < CritChance;
            if(IsCritical)
            {
                damageDone *= 2;
            }

            // check bleed chance
            if(items.Any(i => i.Name == "Dagger"))
            {
                int chancesToBleed = items.First(i => i.Name == "Dagger").Quantity*5;
                if(Random.Range(1, 101) < chancesToBleed)
                {
                    additionalEffects.Add("Bleed");
                }
            }

           damages.Add(new DamageLine{Damage = damageDone, IsCritical = IsCritical, AdditionalEffects = additionalEffects});
        }

        return damages;
    }

    public void UseEquipment()
    {
        if(EquipmentCurrentCooldown == 0)
        {
            Debug.Log("Using equipment - Pineapple");
            
            int previousHealth = Health;
            int HpAfterHeal = Health + 40;
            if(HpAfterHeal > MaxHealth)
            {
                Health = MaxHealth;
            }
            else
            {
                Health = HpAfterHeal;
            }
            Debug.Log("You healed " + (Health - previousHealth) + " HP");
            EquipmentCurrentCooldown = EquipmentCooldown;
        }
        else
        {
            Debug.Log("Equipment is on cooldown");
        }
    }

    public void TakeDamage(DamageLine damage)
    {
        previousHealth = Health;
        Health -= damage.Damage;
        if (Health < 0)
        {
            Health = 0;
        }
        // Show a text effect that disappears after a few seconds
        if(damage.IsCritical)
        {
            ShowTextEffect("-" + damage.Damage + " !", Color.yellow);
        }
        else
        {
            ShowTextEffect("-" + damage.Damage, Color.red);
        }

        foreach(string effect in damage.AdditionalEffects)
        {
            ShowTextEffect(effect, Color.blue);
        }

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

    public void EndOfTurn()
    {
        if(EquipmentCurrentCooldown > 0)
        {
            EquipmentCurrentCooldown--;
        }
    }
}
