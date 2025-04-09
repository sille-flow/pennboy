using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo2 : MonoBehaviour
{
    public Transform goal;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < 0.1f)
        {
            /*
            GetComponent<"Patrol">enabled = true;
            GetComponent<"Patrol">center = goal;
            GetComponent<"MoveTo">enabled = false;
            */
        }
    }
}
