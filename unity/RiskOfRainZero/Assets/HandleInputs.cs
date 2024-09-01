using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Characters
    {
        COMMANDO,
        MICHEL,
        SECRET
    }
public class HandleInputs : MonoBehaviour
{
    
    public GameManager gameManager;
    Characters charcterSelected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttackClicked(int attackNumber)
    {
        Debug.Log("Attack button clicked");
        Debug.Log("Attack Number: " + attackNumber);
        Debug.Log("Character Selected: " + charcterSelected);

        switch(charcterSelected)
        {
            case Characters.COMMANDO:
                CommandoAttack(attackNumber);
                break;
            case Characters.MICHEL:
                MichelAttack(attackNumber);
                break;
            case Characters.SECRET:
                SecretAttack(attackNumber);
                break;
            default:
                Debug.Log("Invalid character selected");
                break;
        }      
    }

    void CommandoAttack(int attackNumber)
    {
        switch(attackNumber)
        {
            case 1:
                Debug.Log("Commando Attack 1");
                gameManager.inflictDamage(true, "Lemurian", 10);
                break;
            case 2:
                Debug.Log("Commando Attack 2");
                break;
            case 3:
                Debug.Log("Commando Attack 3");
                break;
            case 4:
                Debug.Log("Commando Attack 3");
                break;
            default:
                Debug.Log("Invalid attack number");
                break;
        }
    }

    void MichelAttack(int attackNumber)
    {
        switch(attackNumber)
        {
            case 1:
                Debug.Log("Michel Attack 1");
                break;
            case 2:
                Debug.Log("Michel Attack 2");
                break;
            case 3:
                Debug.Log("Michel Attack 3");
                break;
            default:
                Debug.Log("Invalid attack number");
                break;
        }
    }

    void SecretAttack(int attackNumber)
    {
        switch(attackNumber)
        {
            case 1:
                Debug.Log("Secret Attack 1");
                break;
            case 2:
                Debug.Log("Secret Attack 2");
                break;
            case 3:
                Debug.Log("Secret Attack 3");
                break;
            default:
                Debug.Log("Invalid attack number");
                break;
        }
    }
}