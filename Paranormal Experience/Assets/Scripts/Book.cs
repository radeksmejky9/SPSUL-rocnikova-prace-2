using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public GameObject openedBook;
    public GameObject closedBook;
    public GameObject bookContent;
    public Material[] m = new Material[4];
    public bool open;
    public bool written = false;

    public void OpenTheBook()
    {
        open = true;
        closedBook.SetActive(false);
        openedBook.SetActive(true);
    }

    public void CloseTheBook()
    {
        open = false;
        openedBook.SetActive(false);
        closedBook.SetActive(true);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (open == true)
        {
            if (other.tag == "Enemy" && written == false)
            {
                written = true;
                bookContent.GetComponent<MeshRenderer>().materials = new Material[] { m[Random.Range(0, 4)] };
            }
        }
    }
}
