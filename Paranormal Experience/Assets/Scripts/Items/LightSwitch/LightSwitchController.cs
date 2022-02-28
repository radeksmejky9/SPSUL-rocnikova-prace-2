using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchController : MonoBehaviour
{
    public RaycastHit hit;
    public Camera cam;

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 5))
            {
                if (hit.transform.tag == "LightSwitch")
                {
                    hit.transform.gameObject.GetComponent<LightSwitch>().Switch();
                }
            }
        }*/
    }
}
