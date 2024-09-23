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
    public MessagePanelManager messagePanelManager;
    Characters charcterSelected;
    private Coroutine hidePanelCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check for mouse click
        if (messagePanelManager.CanDisplay && Input.GetMouseButtonDown(0))
        {
            // Hide or deactivate the MessagePanelManager panel
            if (messagePanelManager != null)
            {
                messagePanelManager.HideMessage();
                if(hidePanelCoroutine != null)
                {
                    StopCoroutine(hidePanelCoroutine);
                }
            }
        }
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

        hidePanelCoroutine = StartCoroutine(HidePanelAfterDelay(2f));
    }

    private IEnumerator HidePanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Hide or deactivate the MessagePanelManager panel
        if (messagePanelManager != null)
        {
            messagePanelManager.HideMessage();
        }

        hidePanelCoroutine = null;
    }

    void CommandoAttack(int attackNumber)
    {
        switch(attackNumber)
        {
            case 1:
                Debug.Log("SHOOT");
                gameManager.inflictDamage(true, "Lemurian", 10);
                messagePanelManager.SetMessage("Commando fire a shot at Lemurian, it deals " + 10 + " damage !");
                messagePanelManager.DisplayMessage();
                break;
            case 2:
                Debug.Log("BANG");
                gameManager.inflictDamage(true, "Lemurian", 50);
                messagePanelManager.SetMessage("BANG !, it deals " + 50 + " damage !");
                messagePanelManager.DisplayMessage();
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
