using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class Menu : MonoBehaviour
{
    public TMP_Text statText;
    List<bool> values = new List<bool>() { false, false, false, false, false };
    public Image[] images;
    Color ColorOn = new Color(0, 255, 0);
    Color ColorOff = new Color(255, 0, 0);
    public TMP_Text text;
    public Ghost ghost;
    public bool gameDone = false;

    private void Start()
    {
        if (Stats.Instance.statsData == null)
        {
            if (SaveSystem.CheckIfFolderExists)
            {
                if (SaveSystem.CheckIfFileExists("MainSave"))
                {
                    SaveSystem.LoadPlayer("MainSave");
                }
                else
                {
                    Stats.Instance.statsData = new StatsData(0, 0, 0, 0, 0, 0, 0, 0, 0);
                }
            }
            else
            {
                SaveSystem.CreateSaveFolder();
            }
        }
    }
    private void Update()
    {

        if (Stats.Instance.statsData.GameCount == 0)
        {
            statText.text = "Game count:  " + 0 + "\n" +
                   "Winrate:  " + 0 + "\n" +
                   "Salts placed:  " + Stats.Instance.statsData.SaltsPlaced.ToString() + "\n" +
                   "Fingerprint count:  " + Stats.Instance.statsData.SaltProcs.ToString() + "\n" +
                   "Books placed:  " + Stats.Instance.statsData.BooksPlaced.ToString() + "\n" +
                   "Ghost writing count:  " + Stats.Instance.statsData.GhostWritingCount.ToString() + "\n" +
                   "High EMF records:  " + Stats.Instance.statsData.EmfCount.ToString() + "\n" +
                   "Freezing temperature count:  " + Stats.Instance.statsData.FreezingTempsCount.ToString() + "\n" +
                   "Ghost event count: " + Stats.Instance.statsData.GhostEventCount.ToString();
        }
        else
        {
            statText.text = "Game count:  " + (Stats.Instance.statsData.GameCount.ToString()) + "\n" +
                   "Winrate:  " + Math.Floor(((Double)Stats.Instance.statsData.WinCount / (Double)Stats.Instance.statsData.GameCount * 100.0)).ToString() + " %\n" +
                   "Salts placed:  " + Stats.Instance.statsData.SaltsPlaced.ToString() + "\n" +
                   "Fingerprint count:  " + Stats.Instance.statsData.SaltProcs.ToString() + "\n" +
                   "Books placed:  " + Stats.Instance.statsData.BooksPlaced.ToString() + "\n" +
                   "Ghost writing count:  " + Stats.Instance.statsData.GhostWritingCount.ToString() + "\n" +
                   "High EMF records:  " + Stats.Instance.statsData.EmfCount.ToString() + "\n" +
                   "Freezing temperature count:  " + Stats.Instance.statsData.FreezingTempsCount.ToString() + "\n" +
                   "Ghost event count: " + Stats.Instance.statsData.GhostEventCount.ToString();
        }

    }
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

                if (won)
                {
                    Stats.Instance.statsData.WinCount++;
                }
                Stats.Instance.statsData.GameCount++;
                text.text = won ? "Congratulations you won the game!" : "You Lost!";

                SaveSystem.SavePlayer("MainSave");
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


