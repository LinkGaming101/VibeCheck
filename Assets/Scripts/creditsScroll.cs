using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class creditsScroll : MonoBehaviour
{
    public GameObject credits;
    public GameObject panel;

    public GameObject creditButtons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (credits.activeInHierarchy)
        {
            credits.transform.Translate(Vector3.up * 1);
            Vector3[] worldcorner = new Vector3[4];
            panel.GetComponent<RectTransform>().GetWorldCorners(worldcorner);
            Vector3 topleft = worldcorner[1];
            if (credits.transform.position.y > topleft.y)
            {
                creditButtons.SetActive(true);
                Debug.Log("above panel");
            }
        }
    }

    public void showCredits()
    {
        credits.GetComponent<RectTransform>().anchoredPosition = new Vector3(credits.GetComponent<RectTransform>().anchoredPosition.x, -2733, 0);
        credits.SetActive(true);
        panel.SetActive(true);
        creditButtons.SetActive(false);
    }

    public void hideCredits()
    {
        credits.SetActive(false);
        panel.SetActive(false);
        creditButtons.SetActive(false);
    }



}
