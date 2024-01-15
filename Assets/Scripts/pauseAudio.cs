using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject ui;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ui.activeInHierarchy && audioSource.isPlaying)
        {
            audioSource.Pause();
            Debug.Log("not playing");
        }
        else if (!ui.activeInHierarchy && !audioSource.isPlaying)
        {
            audioSource.Play();
            Debug.Log("playing");
        }
    }
}
