using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTank2 : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Tank")
            Destroy(other.gameObject);
        if (other.transform.tag == "Wall")
            Destroy(this.gameObject);
    }
}
