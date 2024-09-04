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

        // Select a foe 
        int dice = Random.Range(1, 101);
        CharacterData enemyData = new CharacterData();
        if(dice < 50)
        {
            enemyData.ImgName = "lemurian";
            enemyData.MaxHealth = 80;
            enemyData.Name = "Lemurian";
        }
        else if (dice < 80)
        {
            enemyData.ImgName = "millenialTurtle";
            enemyData.MaxHealth = 130;
            enemyData.Name = "Millenial Turtle";
        }
        else
        {
            enemyData.ImgName = "wisp";
            enemyData.MaxHealth = 50;
            enemyData.Name = "Flame wisp";
        }

        Enemy.GetComponent<CharacterManager>().LoadCharacterData(enemyData);
    }

    // Update is called once per frame
    void Update()
    {
        
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

    void CheckDeath()
    {
        // if(Hero.GetComponent<CharacterManager>().Health <= 0)
        // {
        //     Debug.Log("Hero is dead");


        // }
        // else if(Enemy.GetComponent<CharacterManager>().Health <= 0)
        // {
        //     Debug.Log("Enemy is dead");
        //     messagePanelManager.SetMessage("");

        //     // for now reload an enemy

        //     Enemy.GetComponent<CharacterManager>()
        // }
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
