using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salt : Item, IPlaceable
{

    public int charges = 3;
    public GameObject salt;

    public void Place(RaycastHit hit)
    {
        if (charges > 0)
        {

            Instantiate(salt, new Vector3(hit.point.x, hit.point.y, hit.point.z), new Quaternion(0, 0, 0, 0));
            charges--;
        }
    }

}
