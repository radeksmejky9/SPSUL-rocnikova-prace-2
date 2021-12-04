using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thermometer : MonoBehaviour
{
    public GameObject text1;
    public GameObject text2;
    private bool is_enabled = false;
    public AudioSource switch_sound;
    private bool rdy = true;

    System.Random r = new System.Random();

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

        this.SetActive(is_enabled);

        if (switch_sound != null)
            switch_sound.Play();
    }

    private void SetActive(bool is_enabled)
    {
        text1.SetActive(is_enabled);
        text2.SetActive(is_enabled);
    }

    IEnumerator changeTemp()
    {
        rdy = false;
        //if freezing bum 
        text1.GetComponent<Text>().text = r.Next(-3, -2).ToString() + "." + r.Next(2, 9).ToString();

        //else bum

        yield return new WaitForSeconds(r.Next(0, 3));
        rdy = true;
    }


    void Update()
    {
        if (is_enabled)
        {
            RaycastHit hit;
            if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, 20))
            {
                if (rdy)
                    StartCoroutine(changeTemp());
            }
            else
            {
                if (rdy)
                    text1.GetComponent<Text>().text = "-.-";
            }
        }

    }
}
