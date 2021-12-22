using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairItemDetection : MonoBehaviour
{
    public bool slotFull = false;
    public RaycastHit hit;
    public Camera cam;
    public float pickUpRange;
    GameObject equippedItem;
    public Transform equipPosition;

    private void Update()
    {

        if (!slotFull && Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, pickUpRange) && Input.GetKeyDown(KeyCode.E))
        {
            if (hit.transform.tag == "Grabbable")
            {
                slotFull = true;
                equippedItem = hit.transform.gameObject;
                equippedItem.transform.position = equipPosition.position;
                equippedItem.transform.parent = equipPosition;
                equippedItem.transform.localEulerAngles = new Vector3(0, -97.32f, 6.1f);
                equippedItem.GetComponent<Rigidbody>().isKinematic = true;
                equippedItem.GetComponent<CapsuleCollider>().enabled = false;

            }
        }

        if (slotFull && Input.GetKeyDown(KeyCode.G))
        {
            equippedItem.transform.parent = null;
            equippedItem.GetComponent<Rigidbody>().isKinematic = false;
            equippedItem.GetComponent<CapsuleCollider>().enabled = true;
            equippedItem = null;
            slotFull = false;
        }

        if (slotFull && Input.GetKeyDown(KeyCode.Mouse1))
        {
            equippedItem.GetComponent<Flashlight>().Switch();
        }
    }
}
