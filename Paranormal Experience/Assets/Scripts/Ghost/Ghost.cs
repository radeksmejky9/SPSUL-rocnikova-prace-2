using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Dictionary<Evidence, bool> activeEvidences;

    public enum Evidence
    {
        EMF,
        GhostWriting,
        Footprints,
        Freezing,
        GhostOrb
    }

    private void Awake()
    {
        activeEvidences = new Dictionary<Evidence, bool>();
        bool[] b = new bool[Enum.GetNames(typeof(Evidence)).Length];

        for (int i = 0; i < b.Length; i++)
        {
            b[i] = false;
        }


        for (int i = 0; i < 3; i++)
        {
            int r = UnityEngine.Random.Range(0, b.Length);
            if (b[r])
            {
                i--;
            }
            else
            {
                b[r] = true;
            }

        }


        int j = 0;
        foreach (Evidence e in Enum.GetValues(typeof(Evidence)))
        {
            activeEvidences.Add(e, b[j]);
            j++;
        }




        foreach (var item in activeEvidences)
        {
            Debug.Log(item.Key + " - " + item.Value);
        }
    }
}
