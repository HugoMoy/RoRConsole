using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Button attackButton1;
    public Button attackButton2;
    public Button attackButton3;
    public Button attackButton4;
    public GameObject GameOverCanvas;
    public GameObject Hero;
    public GameObject Enemy;
    public MessagePanelManager messagePanelManager;
    public EventManager eventManager;

    // XP - START
    public Slider xpSlider;
    private int currentXP = 60;
    private int xpForNextLevel = 100;
    private Image xpfillImage; 
    private bool startOfEvent;
    // XP - END

    public GameEventType currentEvent;

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
                    Name = "Commando",
                    Strength = 10,
                    CritChance = 10
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

        startOfEvent = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        if(startOfEvent)
        {
            currentEvent = eventManager.LoadNextEvent();
        }     
        if(currentEvent == GameEventType.ENEMY)
        {
            // check transition 
            if(startOfEvent)
            {
                Enemy.SetActive(true);
                // Wait 2 seconds before starting the fight
                // StartCoroutine(eventManager.Wait(2));
            
                CharacterData enemyData = eventManager.GetRandomEnemy();
                // load new enemy
                Enemy.GetComponent<CharacterManager>().LoadCharacterData(enemyData);
                
                messagePanelManager.SetMessage("An angry " + enemyData.Name + " appeared!");
                messagePanelManager.DisplayMessage();

                startOfEvent = false;
            }

            CheckDeath();
        }
        else if(currentEvent == GameEventType.SMALL_CHEST || currentEvent == GameEventType.NORMAL_CHEST || currentEvent == GameEventType.BIG_CHEST)
        {
            // check transition
            if(startOfEvent)
            {   
                messagePanelManager.SetMessage("You encounter a chest !");
                messagePanelManager.DisplayMessage();             
                // hide enemyTemplate
                Enemy.SetActive(false);
                startOfEvent = false;
            }
        }
        else if(currentEvent == GameEventType.ALTAR)
        {
            // check transition
            // if(startOfEvent)
            // {
            //     // Wait 2 seconds before starting the fight
            //     StartCoroutine(eventManager.Wait(2));
            //     startOfEvent = false;
            // }
        }
    }

    public void OpenChest()
    {
        messagePanelManager.SetMessage("You open the chest and find a dagger !");
        messagePanelManager.DisplayMessage();
        // give a random loot
        Hero.GetComponent<CharacterManager>().AddItem(new Item(){Name="Dagger", ImgName="dagger", Quantity=1});
        startOfEvent = true;
    }

    public void LeaveChest()
    {
        messagePanelManager.SetMessage("You leave the chest !");
        messagePanelManager.DisplayMessage();
        startOfEvent = true;
    }

    public void ennemyArrival()
    {

        // wait 1 seconds

        // Load new enemy
        CharacterData enemyData = eventManager.GetRandomEnemy();
        Enemy.GetComponent<CharacterManager>().LoadCharacterData(enemyData); 

        // Display message saying a new foe arrived
        messagePanelManager.SetMessage("An angry " + enemyData.Name + " appeared!");
        messagePanelManager.DisplayMessage();       

        // Make enemy picture appear on the right and come to the right side
        // Enemy.GetComponent<CharacterManager>().HideCharacterHealthBar();      
        // Enemy.GetComponent<CharacterManager>().ShowCharacterArriveFromLeft();  
        // Enemy.GetComponent<CharacterManager>().ShowCharacterHealthBar(); 
    }

    public void inflictDamage(bool isHero, string attackText, int strPercentage, int iterations)
    {
        if (isHero)
        {
            string damageText = "";
            // get damages done from hero 
            List<DamageLine> damages = Hero.GetComponent<CharacterManager>().ComputeDamage(strPercentage, iterations);
            CharacterManager characterManager = Enemy.GetComponent<CharacterManager>();
            
            damageText += "(";
            int iter = 0;
            foreach (DamageLine damage in damages)
            {
                if(iter > 0  && iter < damages.Count)
                {
                    damageText += "+ ";
                }

                characterManager.TakeDamage(damage);
                damageText += damage.Damage + " ";
                iter++;
            }
            damageText += ")";

            messagePanelManager.SetMessage(attackText + " " + damageText + " damages !");
            messagePanelManager.DisplayMessage();
        }
        else
        {            
            DamageLine dmg = Enemy.GetComponent<CharacterManager>().ComputeDamage(100, 1).First();
            CharacterManager characterManager = Hero.GetComponent<CharacterManager>();
            characterManager.TakeDamage(dmg);
            
            messagePanelManager.SetMessage("Foe is attacking, it deals " + dmg.Damage + " damage !");
            messagePanelManager.DisplayMessage();

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
            GameOverCanvas.SetActive(true);
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

            startOfEvent = true;
        }
    }

    public void EnemyTurn()
    {

        // roll damage 
        inflictDamage(false, "claw", 100, 1);
    }

    public void UseEquipment()
    {

        messagePanelManager.SetMessage("You use your equipment !");
        messagePanelManager.DisplayMessage();
        Hero.GetComponent<CharacterManager>().UseEquipment();
    }

    public void Flee()
    {
        messagePanelManager.SetMessage("You flee the battle !");
        messagePanelManager.DisplayMessage();
        //eventManager.LoadNextEvent();
    }

    public void Wait()
    {

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
