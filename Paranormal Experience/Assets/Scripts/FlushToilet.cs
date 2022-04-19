using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlushToilet : Item, ISwitchable, ITriggerable
{
    public float flushCooldown = 45.0f;
    public float timer;
    public AudioSource flushSound;
    private void Update()
    {
        timer += Time.deltaTime;
    }
    public void Switch()
    {
        if (timer > flushCooldown)
        {
            timer = 0;
            flushSound.Play();
        }
    }

    public void Trigger(Collider other)
    {
        if (other.tag == "Enemy")
        {
            this.Switch();
            Stats.Instance.statsData.GhostEventCount++;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Trigger(other);
    }
}
