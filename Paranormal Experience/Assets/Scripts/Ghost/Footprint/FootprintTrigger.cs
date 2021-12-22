using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootprintTrigger : MonoBehaviour
{
    bool triggeredAlready = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (!triggeredAlready)
            {
                triggeredAlready = true;
                other.GetComponent<GhostMovement>().FootprintSpammer();
            }
        }
    }
}
