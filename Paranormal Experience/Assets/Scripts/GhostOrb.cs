using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostOrb : MonoBehaviour
{
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Enemy").GetComponent<Ghost>().activeEvidences.TryGetValue(Ghost.Evidence.GhostOrb, out bool ghostOrb))
        {
            if (!ghostOrb)
                this.gameObject.active = false;
        }

    }
}
