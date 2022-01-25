using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicThrowableItem : Item, ITriggerable
{
    public int maxThrowDistance = 50;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
            Trigger(other);
    }

    public void Trigger(Collider other)
    {
        this.GetComponent<Rigidbody>().AddForce(new Vector3(UnityEngine.Random.Range(0, maxThrowDistance), UnityEngine.Random.Range(0, maxThrowDistance), UnityEngine.Random.Range(0, maxThrowDistance)));
    }
}
