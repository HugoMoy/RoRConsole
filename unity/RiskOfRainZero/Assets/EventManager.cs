using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public int chestleft = 3;
    public int findChestOdd = 25;
    private int chestOddBonus = 0;
    public int altarsLeft = 1;
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
