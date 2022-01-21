using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Item, ISwitchable, ITriggerable
{
    bool is_enabled = true;

    public GameObject[] lights;
    public AudioSource asource;


    public void Switch()
    {
        asource.Play();
        is_enabled = !is_enabled;

        for (int i = 0; i < lights.Length; i++)
        {
            if (is_enabled)
            {
                lights[i].GetComponent<Light>().intensity = 3;
            }
            else
            {
                lights[i].GetComponent<Light>().intensity = 0;
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Trigger(other);
    }

    public void Trigger(Collider other)
    {
        if (is_enabled && other.tag == "Enemy")
        {
            this.Switch();
        }
    }

}
