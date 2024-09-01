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
}
