using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Radio : Item, ISwitchable, ITriggerable
{

    public GameObject time;
    public GameObject[] display;
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
        foreach (var item in display)
        {
            item.SetActive(is_enabled);
        }
    }

    public bool Is_Enabled
    {
        get
        {
            return is_enabled;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Trigger(other);
    }

    public void Trigger(Collider other)
    {
        if (!is_enabled && other.tag == "Enemy") {
            this.Switch();
        }
    }

}
