using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostMovement : MonoBehaviour
{
    public float wanderRadius;
    public float wanderTimer;
    public GameObject footprint;

    private Transform target;
    public NavMeshAgent agent;
    private float timer;
    int a;

    private void Start()
    {
        FootprintSpammer();
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
        StartCoroutine("LeaveFootprint");
    }

    IEnumerator LeaveFootprint()
    {
        Debug.Log("footprint created");
        Instantiate(footprint, new Vector3(this.transform.position.x, 19.21f, this.transform.position.z), Quaternion.Euler(new Vector3(0, this.transform.eulerAngles.y, 0)));
        yield return new WaitForSeconds(1.5f);
        a++;
        if (a < 10)
            StartCoroutine("LeaveFootprint");
    }

}
