using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject[] waypoints;

    int patrolWP = 0;
    public float range;

    // Start is called before the first frame update
    void Start()
    {
        //Random position
        agent.transform.position = waypoints[Random.Range(0, waypoints.Length)].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < range) Patrolling();
    }

    void Patrolling()
    {
        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        patrolWP = (patrolWP + 1) % waypoints.Length;
        Seek();
    }

    void Seek()
    {
        if (waypoints.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = waypoints[patrolWP].transform.position;
        //agent.SetDestination(waypoints[patrolWP].transform.position;
    }

}
