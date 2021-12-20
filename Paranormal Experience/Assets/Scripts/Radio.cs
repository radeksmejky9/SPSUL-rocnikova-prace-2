using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Radio : MonoBehaviour
{

    public GameObject time;
    public AudioSource audioSource;

    bool is_enabled;

    private void Update()
    {
        DateTime currentTime = DateTime.Now;
        if (currentTime.Minute < 10)
        {
            time.GetComponent<Text>().text = currentTime.Hour + ":0" + currentTime.Minute;
        }
        else
        {
            time.GetComponent<Text>().text = currentTime.Hour + ":" + currentTime.Minute;
        }

    }


    public bool Is_Enabled
    {
        get
        {
            return is_enabled;
        }
    }
    public void Switch()
    {
        is_enabled = !is_enabled;
        if (is_enabled)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }

        time.SetActive(is_enabled);
    }


}
