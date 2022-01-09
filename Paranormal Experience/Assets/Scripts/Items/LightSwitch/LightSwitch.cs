using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Item, ISwitchable
{
    bool IsOn = true;

    public GameObject[] lights;
    public AudioSource asource;


    public void Switch()
    {
        asource.Play();
        IsOn = !IsOn;

        for (int i = 0; i < lights.Length; i++)
        {
            if (IsOn)
            {
                lights[i].GetComponent<Light>().intensity = 3;
            }
            else
            {
                lights[i].GetComponent<Light>().intensity = 0;
            }

        }
    }

}
