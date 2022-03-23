using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    List<bool> values = new List<bool>() { false, false, false, false, false };
    public Image[] images;
    Color ColorOn = new Color(0, 255, 0);
    Color ColorOff = new Color(255, 0, 0);
    public TMP_Text text;
    public Ghost ghost;
    public bool gameDone = false;

    public void Switch(int index)
    {
        int trueCount = GetTrueCount();
        if (trueCount < 3)
        {
            values[index] = values[index] ? false : true;
            images[index].color = values[index] ? ColorOn : ColorOff;
        }
        else
        {
            values[index] = false;
            images[index].color = ColorOff;
        }
    }

    private int GetTrueCount()
    {
        int trueCount = 0;
        foreach (var val in values)
        {
            if (val)
                trueCount++;
        }
        return trueCount;
    }

    public void EndTheGame()
    {
        if (!gameDone)
        {
            gameDone = true;
            int trueCount = GetTrueCount();
            List<bool> actualEvidences = new List<bool>();

            foreach (var item in ghost.activeEvidences.Values)
            {
                actualEvidences.Add(item);
            }

            if (trueCount == 3)
            {
                bool won = true;
                for (int i = 0; i < actualEvidences.Count; i++)
                {
                    if (actualEvidences[i] != values[i])
                        won = false;
                }

                text.text = won ? "Congratulations you won the game!" : "You Lost!";


                StartCoroutine(WaitForSceneLoad());

            }


            IEnumerator WaitForSceneLoad()
            {
                yield return new WaitForSeconds(5);
                SceneManager.LoadScene(0);
            }
        }
    }
}


