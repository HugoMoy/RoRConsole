using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public enum EventType
    {
        SMALL_CHEST,
        NORMAL_CHEST,
        BIG_CHEST,
        ALTAR,
        TELEPORTER,
        ENEMY
    }

    public int chestleft = 3;
    public int findChestOdd = 25;
    private int chestOddBonus = 0;
    public int altarsLeft = 1;
    public int findAltarOdd = 25;
    private int altarOddBonus = 0;
    public bool hasTeleporterBeenFound = false;
    public int findTeleporterOdd = 25;

    private EventType currentEvent;


    // Start is called before the first frame update
    void Start()
    {
        currentEvent = EventType.ENEMY;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public EventType LoadNextEvent()
    {
        if(chestleft > 0)
        {
            int dice = Random.Range(1, 101);
            if(dice < findChestOdd + chestOddBonus) 
            {
                chestleft--;
                return EventType.SMALL_CHEST;                
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
                return EventType.ALTAR;
            }
            else
            {
                altarOddBonus += 5;
            }
        }

        return EventType.ENEMY;
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

        return enemyData;
    }

    public EventType CurrentEvent()
    {
        return EventType.ENEMY;
        //return currentEvent;
    }
}
