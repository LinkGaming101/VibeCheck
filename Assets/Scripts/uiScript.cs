using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class uiScript : MonoBehaviour
{
    [SerializeField]
    string nextScene;

    public bool resumeGame;

    public GameObject panel;
    public GameObject menuBox;

    [Header("Volume Slider")]
    public Slider volumeBGM;
    public TextMeshProUGUI volumeBGMString;
    public AudioSource asBGM;
    public Slider volumeVibeCheck;
    public TextMeshProUGUI volumeVCString;
    public AudioSource asVC;

    public TMP_Dropdown ddResolution;

    public KeyCode showPause;

    public GameObject dialogueBox;

    public GameObject pauseButton;

    public GameObject howToPlay;

    public GameObject credits;
    public GameObject panelCred;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Main_Menu")
        {
            volumeBGM.value = PlayerPrefs.GetFloat("BGM_Volume", volumeBGM.value);
            volumeVibeCheck.value = PlayerPrefs.GetFloat("VC_Volume", volumeVibeCheck.value);
            ddResolution.value = PlayerPrefs.GetInt("ResolutionOption", ddResolution.value);
        }
        if (volumeBGM != null && volumeVibeCheck != null && ddResolution != null)
        { 
            onBGMChange();
            onVCChange();
            changeResolution();
        }

        //player prefs to save volume and resolution
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueBox != null)
        {
            if (Input.GetKeyDown(showPause) && !dialogueBox.activeInHierarchy)
            {
                resume();
            }
        }
        else
        {
            if (Input.GetKeyDown(showPause))
            {
                resume();
            }
        }
    }

    public void startGame()
    {
        //loads the next scene
        SceneManager.LoadScene(nextScene);
    }

    public void exitGame()
    {
        //exits game
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void onBGMChange()
    {
        //updates the volume textbox for BGM
        volumeBGMString.text = "BGM: " + Mathf.FloorToInt((volumeBGM.value * 100)).ToString();
        asBGM.volume = volumeBGM.value;
        //saves to player prefs
        PlayerPrefs.SetFloat("BGM_Volume", volumeBGM.value);
        //updates BGM volume here
    }

    public void onVCChange()
    {
        //updates the volume textbox for VC
        volumeVCString.text = "Rhythm: " + Mathf.FloorToInt((volumeVibeCheck.value * 100)).ToString();
        asVC.volume = volumeVibeCheck.value;
        //saves to player prefs
        PlayerPrefs.SetFloat("VC_Volume", volumeVibeCheck.value);
        //updates VC volume here
    }

    public void changeResolution()
    {
        Debug.Log(ddResolution.options[ddResolution.value].text);
        string[] breakdown = ddResolution.options[ddResolution.value].text.ToString().Split(" x ");

        int x = int.Parse(breakdown[0]);
        int y = int.Parse(breakdown[1]);

        PlayerPrefs.SetInt("ResolutionOption", ddResolution.value);

#if UNITY_EDITOR
        Debug.Log(breakdown[0]);
        Debug.Log(breakdown[1]);
#endif
        Screen.SetResolution(x, y, false);
    }
    public void resume()
    {
        //opens the pause menu and stops the game
        if (resumeGame)
        {
            panel.SetActive(true);
            menuBox.SetActive(true);

            Time.timeScale = 0;
            pauseButton.SetActive(false);
            resumeGame = false;
        }

        else if (!resumeGame)
        {
            panel.SetActive(false);
            menuBox.SetActive(false);
            pauseButton.SetActive(true);

            Time.timeScale = 1;
            resumeGame = true;
        }
    }

    public void showHowToPlay()
    {
        howToPlay.SetActive(true);
    }

    public void hideHowToPlay()
    {
        howToPlay.SetActive(false);
    }

    public void showCredits()
    {
        credits.SetActive(true);
        panelCred.SetActive(true);
    }

    public void hideCredits()
    {
        credits.SetActive(false);
        panelCred.SetActive(false);
    }
}
