using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public Ghost ghost;
    public GameObject openedBook;
    public GameObject closedBook;
    public GameObject bookContent;
    public Material[] m = new Material[4];
    public bool open;
    public bool written = false;
    public AudioSource audio;
    public bool gw;

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
    private void Start()
    {
        if (ghost.activeEvidences.TryGetValue(Ghost.Evidence.GhostWriting, out bool ghostwriting))
        {
            gw = ghostwriting;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (open && gw)
        {
            if (other.tag == "Enemy" && !written)
            {
                written = true;
                audio.Play();
                bookContent.GetComponent<MeshRenderer>().materials = new Material[] { m[Random.Range(0, 4)] };
            }
        }
    }
}

