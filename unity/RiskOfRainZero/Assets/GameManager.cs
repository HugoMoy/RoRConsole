using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Button attackButton1;
    public Button attackButton2;
    public Button attackButton3;
    public Button attackButton4;
    public GameObject Hero;
    public GameObject Enemy;
    public MessagePanelManager messagePanelManager;
    public EventManager eventManager;

    // XP - START
    public Slider xpSlider;
    private int currentXP = 60;
    private int xpForNextLevel = 100;
    private Image xpfillImage; 
    private bool startFight;
    // XP - END

    void Start()
    {
        Characters characterSelected = Characters.COMMANDO;

        switch(characterSelected)
        {
            case Characters.COMMANDO:            
                attackButton1.GetComponentInChildren<Text>().text = "SHOOT";
                attackButton2.GetComponentInChildren<Text>().text = "BANG";
                attackButton3.GetComponentInChildren<Text>().text = "ROLL";
                attackButton4.GetComponentInChildren<Text>().text = "BARILLET";
                Hero.GetComponent<CharacterManager>().LoadCharacterData(new CharacterData
                {
                    ImgName = "NoIMG",
                    MaxHealth = 142,
                    Name = "Commando"
                });
                break;
            case Characters.MICHEL:
                Debug.Log("Michel Selected");
                break;
            case Characters.SECRET:
                Debug.Log("Secret Selected");
                break;
            default:
                Debug.Log("Invalid character selected");
                break;
        }

        if (xpSlider != null)
        {
            // Get the Image component of the Fill area
            xpfillImage = xpSlider.fillRect.GetComponent<Image>();
            xpSlider.maxValue = xpForNextLevel;
            if (xpfillImage != null)
            {
                // Set the color of the Fill area based on the health value
                UpdateXpSlider();
            }
            else
            {
                Debug.LogError("No Image component found in the Fill area.");
            }
        }

        startFight = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        EventManager.EventType currentEvent = eventManager.CurrentEvent();
        if(currentEvent == EventManager.EventType.ENEMY)
        {
            // check transition 
            if(startFight)
            {
                CharacterData enemyData = eventManager.GetRandomEnemy();
                // load new enemy
                Enemy.GetComponent<CharacterManager>().LoadCharacterData(enemyData);
                
                messagePanelManager.SetMessage("An angry " + enemyData.Name + " appeared!");
                messagePanelManager.DisplayMessage();

                startFight = false;
            }

            CheckDeath();
        }
    }

    public void inflictDamage(bool isHero, string enemyName, int damage)
    {
        if (isHero)
        {
            CharacterManager characterManager = Enemy.GetComponent<CharacterManager>();
            characterManager.TakeDamage(damage);
        }
        else
        {
            CharacterManager characterManager = Hero.GetComponent<CharacterManager>();
            characterManager.TakeDamage(damage);
        }
    }



    private void UpdateXpSlider()
    {
        if (xpfillImage != null)
        {
            // Calculate the health percentage
            float xpPercentage = (float)currentXP / xpForNextLevel;

            // Debug.LogError(xpPercentage+" "+currentXP+" "+xpForNextLevel);
            // Set the color to green when health is full and red when health is zero
            xpfillImage.color = Color.Lerp(Color.blue, Color.green, xpPercentage);
            xpSlider.value = currentXP;
        }
        else
        {
            Debug.LogError("No Image component found in the Fill area.");
        }
    }

    void CheckDeath()
    {
        if(Hero.GetComponent<CharacterManager>().Health <= 0)
        {
            Debug.Log("Hero is dead");
        }
        else if(Enemy.GetComponent<CharacterManager>().Health <= 0)
        {
            Debug.Log("Enemy is dead");

            messagePanelManager.SetMessage("Enemy is dead");
            messagePanelManager.DisplayMessage();

            // give a random loot
            Hero.GetComponent<CharacterManager>().AddItem(new Item(){Name="Dagger", ImgName="dagger", Quantity=1});

            currentXP+=30;
            UpdateXpSlider();

            startFight = true;
        }
    }


    public void LoadNextEvent()
    {
        // if(chestleft > 0)
        // {
        //     int dice = Random.Range(1, 101);
        //     if(dice < findChestOdd + chestOddBonus) 
        //     {
        //         chestleft--;
        //         gamemanager.instance.foundChest();
        //         chestOddBonus = 0;
        //     }
        //     else
        //     {   
        //         chestOddBonus += 5;
        //     }            
        // }

        // if(altarsLeft > 0)
        // {
        //     int dice = Random.Range(1, 101);
        //     if (dice < findAltarOdd + altarOddBonus)
        //     {
        //         altarsLeft--;
        //         gamemanager.instance.foundAltar();
        //         altarOddBonus = 0;
        //     }
        //     else
        //     {
        //         altarOddBonus += 5;
        //     }
        // }
    }
}
