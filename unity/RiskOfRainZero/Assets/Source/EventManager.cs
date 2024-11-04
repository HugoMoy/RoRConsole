using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public int chestleft = 3;
    public int findChestOdd = 25;
    private int chestOddBonus = 0;
    public int altarsLeft = 0; // TODO
    public int findAltarOdd = 25;
    private int altarOddBonus = 0;
    public bool hasTeleporterBeenFound = false;
    public int findTeleporterOdd = 25;



    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public GameEventType LoadNextEvent()
    {
        if(chestleft > 0)
        {
            int dice = Random.Range(1, 101);
            if(dice < findChestOdd + chestOddBonus) 
            {
                chestleft--;
                return GameEventType.SMALL_CHEST;                
                chestOddBonus = 0;
            }
            else
            {   
                chestOddBonus += 5;
            }            
        }

        if(altarsLeft > 0)
        {
            int dice = Random.Range(1, 101);
            if (dice < findAltarOdd + altarOddBonus)
            {
                altarsLeft--;
                altarOddBonus = 0;
                return GameEventType.ALTAR;
            }
            else
            {
                altarOddBonus += 5;
            }
        }

        return GameEventType.ENEMY;
    }

    public CharacterData GetRandomEnemy()
    {
        // Select a foe 
        int dice = Random.Range(1, 101);
        CharacterData enemyData = new CharacterData();
        if(dice < 50)
        {
            enemyData.ImgName = "lemurian";
            enemyData.MaxHealth = 80;
            enemyData.Name = "Lemurian";
            enemyData.Strength = 10;
        }
        else if (dice < 80)
        {
            enemyData.ImgName = "millenialTurtle";
            enemyData.MaxHealth = 130;
            enemyData.Name = "Millenial Turtle";
            enemyData.Strength = 7;
        }
        else
        {
            enemyData.ImgName = "wisp";
            enemyData.MaxHealth = 50;
            enemyData.Name = "Flame wisp";
            enemyData.Strength = 20;
        }

        return enemyData;
    }

    // public GameEventType CurrentEvent()
    // {
    //     return GameEventType.ENEMY;
    //     //return currentEvent;
    // }
}
