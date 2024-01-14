using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
    public GameObject panelBox;
    public TextMeshProUGUI playerQuestion;
    public TextMeshProUGUI npcAnswer;
    public List<string> npcDialogue = new List<string>();

    public string fightScene;

    public GameObject pauseButton;
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
                panelBox.SetActive(true);
                pauseButton.SetActive(false);
                if (Input.GetKeyDown(dialogueKey))
                {
                    nextLine();
                }
            }
            else if (pcScript.inDialogue == false)
            {
                panelDialogue.SetActive(false);
                panelBox.SetActive(false);
                pauseButton.SetActive(true);
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

        for (int i = 0; i < pcScript.playerDialogue.Count; i++)
        {
            pcScript.playerDialogue[i] = replaceText(pcScript.playerDialogue[i]);
        } 
        
        for (int i = 0; i < npcDialogue.Count; i++)
        {
            npcDialogue[i] = replaceText(npcDialogue[i]);
        }
        dialogueOption = 0;

        nextLine();
        //start the NPC dialogue
        pcScript.inDialogue = true;
        Debug.Log("Dialogue started!");
    }
    string replaceText(string a)
    {
        if (a.Contains("[NPC Name]"))
        {
            a = a.Replace("[NPC Name]", npcName);
        }  
        
        if (a.Contains("[instrument]"))
        {
            a = a.Replace("[instrument]", npcInstrument);
        }  
        
        if (a.Contains("[characteristic]"))
        {
            a = a.Replace("[characteristic]", npcCharacteristic);
        } 
        
        if (a.Contains("[PC Name]"))
        {
            a = a.Replace("[PC Name]", pcScript.playerName);
        }

        return a;
    }
    public void nextLine()
    {
        if (dialogueOption < pcScript.playerDialogue.Count)
        {

            playerQuestion.text = pcScript.playerDialogue[dialogueOption];
            npcAnswer.text = npcDialogue[dialogueOption];
            dialogueOption++;

        }
        else
        {
            Debug.Log("Start fight");
            SceneManager.LoadScene(fightScene);
            //start fight gameplay here
        }
    }
}
