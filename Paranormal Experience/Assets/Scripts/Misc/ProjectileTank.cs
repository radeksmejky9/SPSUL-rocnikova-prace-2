using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTank : MonoBehaviour
{
    public string tag;

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == tag)
            Destroy(other.gameObject);
        if (other.transform.tag == "Wall")
            Destroy(this.gameObject);
    }
}
