using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostMovement : MonoBehaviour
{
    public float wanderRadius;
    public float wanderTimer;
    public GameObject[] footprints;
    public float InteractionSoundCooldown;
    private AudioSource[] clips;
    bool fp;
    private Transform target;
    public NavMeshAgent agent;
    private float timer;
    int a;

    private void Start()
    {
        if (this.GetComponent<Ghost>().activeEvidences.TryGetValue(Ghost.Evidence.Footprints, out bool footprints))
        {
            fp = footprints;
        }
        clips = this.gameObject.GetComponents<AudioSource>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        InteractionSoundCooldown += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }


    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }


    public void FootprintSpammer()
    {
        if (fp)
        {
            a = 0;
            clips[0].Play();
            StartCoroutine("LeaveFootprint");
        }
    }

    IEnumerator LeaveFootprint()
    {
        Instantiate(footprints[0], new Vector3(this.transform.position.x, 20.1f, this.transform.position.z), Quaternion.Euler(new Vector3(-90, 180 + this.transform.eulerAngles.y, 0)));
        yield return new WaitForSeconds(0.5f);
        Instantiate(footprints[1], new Vector3(this.transform.position.x, 20.1f, this.transform.position.z), Quaternion.Euler(new Vector3(-90, 180 + this.transform.eulerAngles.y, 0)));

        yield return new WaitForSeconds(1f);

        a++;
        if (a < 5)
        {
            StartCoroutine("LeaveFootprint");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && InteractionSoundCooldown >= 60)
        {
            InteractionSoundCooldown = 0;
            clips[1].Play();
        }


    }

}
