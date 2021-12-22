using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Phone : MonoBehaviour
{
    public GameObject cam;
    public GameObject date;
    public GameObject username;
    public GameObject time;
    public GameObject topBarTime;
    public GameObject topBar;
    bool is_enabled;
    private void Awake()
    {
        var s = Environment.UserName.ToString().ToCharArray();
        username.GetComponent<Text>().text = "Hello " + s[0].ToString().ToUpper();

        for (int i = 1; i < s.Length; i++)
        {
            username.GetComponent<Text>().text += s[i];
        }

    }
    private void Update()
    {
        DateTime currentTime = DateTime.Now;
        if (currentTime.Minute < 10)
        {
            time.GetComponent<Text>().text = currentTime.Hour + ":0" + currentTime.Minute;
            topBarTime.GetComponent<Text>().text = currentTime.Hour + ":0" + currentTime.Minute;
        }
        else
        {
            time.GetComponent<Text>().text = currentTime.Hour + ":" + currentTime.Minute;
            topBarTime.GetComponent<Text>().text = currentTime.Hour + ":" + currentTime.Minute;
        }
        date.GetComponent<Text>().text = currentTime.DayOfWeek + ", " + currentTime.ToString("MMMM") + " " + currentTime.Day;
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

        this.SetActive(is_enabled);
    }

    void SetActive(bool is_enabled)
    {
        cam.SetActive(is_enabled);
        time.SetActive(!is_enabled);
        date.SetActive(!is_enabled);
        topBar.SetActive(is_enabled);
        topBarTime.SetActive(is_enabled);
        username.SetActive(!is_enabled);
    }
}