using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TVRemote : MonoBehaviour
{

    public GameObject tv;

    bool is_enabled;


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
            tv.GetComponent<VideoPlayer>().Pause();
            tv.GetComponent<MeshRenderer>().enabled = false;
        }

    }


}
