using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostMovement : MonoBehaviour
{
    public float wanderRadius;
    public float wanderTimer;
    public GameObject[] footprints;
    public AudioSource audio;

    private Transform target;
    public NavMeshAgent agent;
    private float timer;
    int a;

    private void Start()
    {
        //FootprintSpammer();
    }

    void Update()
    {
        timer += Time.deltaTime;

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
        a = 0;
        audio.Play();
        StartCoroutine("LeaveFootprint");
    }

    IEnumerator LeaveFootprint()
    {
        //Debug.Log("footprint created");
        Instantiate(footprints[0], new Vector3(this.transform.position.x, 19.25f, this.transform.position.z), Quaternion.Euler(new Vector3(-90, 180 + this.transform.eulerAngles.y, 0)));
        yield return new WaitForSeconds(0.5f);
        Instantiate(footprints[1], new Vector3(this.transform.position.x, 19.25f, this.transform.position.z), Quaternion.Euler(new Vector3(-90, 180 + this.transform.eulerAngles.y, 0)));

        yield return new WaitForSeconds(1f);

        a++;
        if (a < 5)
        {
            StartCoroutine("LeaveFootprint");
        }
    }

}
