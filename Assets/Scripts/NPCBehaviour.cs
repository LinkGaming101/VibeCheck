using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCBehaviour : MonoBehaviour
{
    [Header("NPC Info")]
    public string npcName;
    public string npcInstrument;
    public string npcCharacteristic;


    [SerializeField]
    PlayerController pcScript;

    [Header("Dialogue Stuff")]
    public KeyCode dialogueKey;
    public int dialogueOption;
    public GameObject panelDialogue;
    public TextMeshProUGUI playerQuestion;
    public TextMeshProUGUI npcAnswer;

    void Start()
    {
        
    }

    void Update()
    {
        Animate();

        if (pcScript != null)
        {
            if (pcScript.inDialogue == true)
            {
                panelDialogue.SetActive(true);

                if (dialogueOption == 0)
                {
                    string text1 = "hello, my name is [PC Name], what is your name";
                    text1 = text1.Replace("[PC Name]", pcScript.playerName);
                    playerQuestion.text = text1;
                    string text2 = "My name is [NPC Name]";
                    text2 = text2.Replace("[NPC Name]", npcName);
                    npcAnswer.text = text2;
                    dialogueOption = 1;
                }
                if (Input.GetKeyDown(dialogueKey))
                {
                    nextLine();
                }
            }
            else if (pcScript.inDialogue == false)
            {
                panelDialogue.SetActive(false);
                pcScript = null;
            }
        }
    }

    void Animate()
    {
        // animation code goes here for idle and constant if any
    }

    public void Dialogue(GameObject plr)
    {
        transform.LookAt(plr.transform);
        pcScript = plr.GetComponent<PlayerController>();
        dialogueOption = 0;
        //start the NPC dialogue
        pcScript.inDialogue = true;
        Debug.Log("Dialogue started!");
    }

    public void nextLine()
    {
        switch (dialogueOption)
        {
            case 1:
                string text1 = "Nice to meet you [NPC Name], what instrument do you like to play";
                text1 = text1.Replace("[NPC Name]", npcName);
                playerQuestion.text = text1;
                string text2 = "I like to play [instrument]";
                text2 = text2.Replace("[instrument]", npcInstrument);
                npcAnswer.text = text2;
                break;
            case 2:
                string text4 = "I need [instrument] in my band, tell me something about you";
                text4 = text4.Replace("[instrument]", npcInstrument);
                playerQuestion.text = text4;
                string text5 = "I [characteristic]";
                text5 = text5.Replace("[characteristic]", npcCharacteristic);
                npcAnswer.text = text5;
                break;
            case 3:
                playerQuestion.text = "Sounds interesting, let's fight";
                npcAnswer.text = " ";
                Debug.Log("starts battle");
                break;
        }

        dialogueOption++;
    }
}
