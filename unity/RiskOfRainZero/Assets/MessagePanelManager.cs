using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanelManager : MonoBehaviour
{
    public int fontSize = 14;
    public Color color = Color.red;
    public float promptSpeed = 0.05f;

    private Text DisplayTextComponent;
    public bool CanDisplay;
    public string TextToDisplay = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";

    private int index = 0;
    private int previousIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        DisplayTextComponent = GetComponentInChildren<Text>();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {     
        if(CanDisplay)
        {
            if(index < TextToDisplay.Length)
            {
                index = (int)Mathf.Lerp(index, TextToDisplay.Length, Time.deltaTime * promptSpeed);
                if(index == previousIndex)
                {
                    index++;
                }
                DisplayTextComponent.text = TextToDisplay.Substring(0, index);
                previousIndex = index;    
            }     
        }
        else
        {
            DisplayTextComponent.text = "";
            index = 0;
            previousIndex = 0;
        }
    }

    public void SetMessage(string message)
    {
        CanDisplay = false;
        TextToDisplay = message;
        DisplayTextComponent.fontSize = fontSize;
        DisplayTextComponent.color = color;
    }

    public void DisplayMessage()
    {
        gameObject.SetActive(true);
        CanDisplay = true;
    }

    public void HideMessage()
    {
        gameObject.SetActive(false);
        CanDisplay = false;
    }
}
