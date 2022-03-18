using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TVRemote : Item, ISwitchable, ITriggerable
{

    public GameObject tv;
    public GameObject eastereggPlane;
    public bool is_enabled;


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
            tv.GetComponent<VideoPlayer>().Play();
            tv.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            eastereggPlane.SetActive(false);
            tv.SetActive(true);
            tv.GetComponent<VideoPlayer>().Pause();
            tv.GetComponent<MeshRenderer>().enabled = false;
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        Trigger(other);
    }

    public void Trigger(Collider other)
    {
        if (!is_enabled && other.tag == "Enemy")
        {
            this.Switch();
        }
    }


}
