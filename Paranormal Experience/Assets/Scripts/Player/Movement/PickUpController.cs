using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public bool slotFull = false;
    public RaycastHit hit;
    public Camera cam;
    public float pickUpRange;
    GameObject equippedItem;
    public Transform equipPosition;
    public Transform equipPosition2;

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
                equippedItem.GetComponent<Collider>().enabled = false;

                if (hit.transform.gameObject.layer == 9)
                {
                    hit.transform.GetComponentInParent<Book>().CloseTheBook();
                }

                if (hit.transform.gameObject.layer == 11)
                {
                    equippedItem.transform.position = equipPosition2.position;
                    equippedItem.transform.parent = equipPosition2;
                    equippedItem.transform.localEulerAngles = new Vector3(87.5f, -180, -3.03f);
                }

            }
        }

        if (slotFull && Input.GetKeyDown(KeyCode.G))
        {
            equippedItem.transform.parent = null;
            equippedItem.GetComponent<Rigidbody>().isKinematic = false;
            equippedItem.GetComponent<Collider>().enabled = true;
            equippedItem.GetComponent<Rigidbody>().AddForce(cam.GetComponent<Transform>().forward * 300);
            equippedItem = null;
            slotFull = false;
        }

        if (slotFull && Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (equippedItem.layer == 8 || equippedItem.layer == 13)
                equippedItem.GetComponent<Flashlight>().Switch();
            if (equippedItem.layer == 10)
                equippedItem.GetComponent<Thermometer>().Switch();
            if (equippedItem.layer == 11)
                equippedItem.GetComponent<Phone>().Switch();
            if (equippedItem.layer == 15)
                equippedItem.GetComponent<Radio>().Switch();
            if (equippedItem.layer == 16)
                equippedItem.GetComponent<TVRemote>().Switch();
            if (equippedItem.layer == 17)
                equippedItem.GetComponent<EMF>().Switch();

        }

        if (slotFull && Input.GetKeyDown(KeyCode.F) && Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, pickUpRange))
        {
            if (equippedItem.layer == 12)
            {
                if (hit.transform.gameObject.tag == "Floor")
                {
                    equippedItem.GetComponent<Salt>().Place(hit);
                }
            }
            if (equippedItem.layer == 9)
            {
                if (hit.transform.gameObject.tag == "Floor")
                {
                    equippedItem.GetComponent<Book>().Place(hit);
                    equippedItem = null;
                    slotFull = false;
                }

            }


        }
    }


}
