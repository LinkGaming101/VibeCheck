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
    public Slider volumeVibeCheck;
    public TextMeshProUGUI volumeVCString;

    public TMP_Dropdown ddResolution;

    public KeyCode showPause;

    // Start is called before the first frame update
    void Start()
    {
        if (volumeBGM != null && volumeVibeCheck != null)
        { 
            onBGMChange();
            onVCChange();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(showPause))
        {
            resume();
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

        //updates BGM volume here
    }

    public void onVCChange()
    {
        //updates the volume textbox for VC
        volumeVCString.text = "VC: " + Mathf.FloorToInt((volumeVibeCheck.value * 100)).ToString();

        //updates VC volume here
    }

    public void changeResolution()
    {
        Debug.Log(ddResolution.options[ddResolution.value].text);
        string[] breakdown = ddResolution.options[ddResolution.value].text.ToString().Split(" x ");

        int x = int.Parse(breakdown[0]);
        int y = int.Parse(breakdown[1]);
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
            resumeGame = false;
        }

        else if (!resumeGame)
        {
            panel.SetActive(false);
            menuBox.SetActive(false);

            Time.timeScale = 1;
            resumeGame = true;
        }
    }
}
