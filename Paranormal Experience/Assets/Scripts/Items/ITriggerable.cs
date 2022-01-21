using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ITriggerable
{
    abstract void Trigger(Collider other);
}
