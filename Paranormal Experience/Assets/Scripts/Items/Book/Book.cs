using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : Item, IPlaceable
{
    public GameObject openedBook;
    public GameObject closedBook;
    public GameObject bookContent;
    public Material[] m = new Material[4];
    public bool open;
    public bool written = false;
    public AudioSource audio;
    public bool gw = true;

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
        if (GameObject.FindGameObjectWithTag("Enemy").GetComponent<Ghost>().activeEvidences.TryGetValue(Ghost.Evidence.GhostWriting, out bool ghostwriting))
        {
            gw = ghostwriting;
        }
    }

    public void Place(RaycastHit hit)
    {
        transform.parent = null;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Collider>().enabled = true;
        GetComponent<Book>().OpenTheBook();
        transform.position = hit.point;
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (open && gw)
        {
            if (other.tag == "Enemy" && !written)
            {
                Stats.Instance.statsData.GhostWritingCount++;
                written = true;
                audio.Play();
                bookContent.GetComponent<MeshRenderer>().materials = new Material[] { m[Random.Range(0, 4)] };
            }
        }
    }
}

