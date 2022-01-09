using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IPlaceable
{
    abstract void Place(RaycastHit hit);
}
