using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    bool IsOn = false;

    public GameObject[] lights;


    public void Switch()
    {
        IsOn = !IsOn;
        this.transform.gameObject.GetComponent<Renderer>().material.color = IsOn == false ? Color.red : Color.green;

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
