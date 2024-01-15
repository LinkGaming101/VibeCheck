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

    public int totalAmount;

    public static int amountDone;
    public static int amountMissed;

    public int total;

    void Start()
    {
        Instance = this;
        comboScore = 0;

        anim = anim2;

    }
    public static void Hit()
    {
        comboScore += 1;
        amountDone += 1;
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

        if (amountDone >= total)
        {
            if (amountMissed >= totalAmount)
            {
                //checks if the player loses
                anim.SetBool("isLose", true);

            }
        }
    }
}
