using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public AudioSource hitSFX;
    public AudioSource missSFX;
    public TMPro.TextMeshProUGUI scoreText;
    static int comboScore;

    public static Animator anim;
    public Animator anim2;

    public int amountAbletoBeMissed;

    public static int amountDone;
    public static int amountMissed;

    public int total;

    public Lane lane1;
    public Lane lane2;
    public Lane lane3;
    public Lane lane4;

    public static bool countingStopped;

    public TMPro.TextMeshProUGUI winLoseText;
    public GameObject winLosemeu;
    public GameObject panel;
    void Start()
    {
        Instance = this;
        comboScore = 0;

        anim = anim2;
        countingStopped = false;

        StartCoroutine(totalCheck());


    }

    public IEnumerator totalCheck()
    {
        yield return new WaitForFixedUpdate();
        total = lane1.timeStamps.Count + lane2.timeStamps.Count + lane3.timeStamps.Count + lane4.timeStamps.Count;
        amountAbletoBeMissed = Mathf.RoundToInt(total / 2);
        yield break;

    }
    public static void Hit()
    {
        comboScore += 1;
        amountDone += 1;
        countingStopped = true;

        //if player hits a beat, play cheer animation
        anim.SetBool("isHappy", false);
        anim.SetBool("isCheering", true);
        Instance.hitSFX.Play();
    }
    public static void Miss()
    {
        comboScore = 0;
        amountDone += 1;
        amountMissed +=1;
        countingStopped = true;

        //if player misses a beat, animation shows the "happy" (actually upset) animation 
        anim.SetBool("isCheering", false);
        anim.SetBool("isHappy", true);
        Instance.missSFX.Play();
    }
    private void Update()
    {
        scoreText.text = comboScore.ToString();

        if (comboScore % 10 == 0 && comboScore > 0)
        {
            //play vibe check animation when the player has 10 in a row
            anim.SetBool("isVibeCheck", true);
        }
        else
        {
            anim.SetBool("isVibeCheck", false);
        }

        if (countingStopped)
        {
            if (total == amountDone)
            {
                if (amountMissed >= amountAbletoBeMissed)
                {
                    //checks if the player loses
                    anim.SetBool("isLose", true);
                    Debug.Log("game ends and loses");
                    winLosemeu.SetActive(true);
                    panel.SetActive(true);
                    winLoseText.text = "You Lose";

                }
                else
                {
                    anim.SetBool("isVibeCheck", true);
                    Debug.Log("game ends and wins");
                    winLosemeu.SetActive(true);
                    panel.SetActive(true);
                    winLoseText.text = "You Win";
                }
            }
        }
    }
}
