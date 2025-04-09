using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{

    public Vector3 [] points;
    private int destPoint = 0;
    private UnityEngine.AI.NavMeshAgent agent;
    private bool alert;
    private bool blocked;
    private UnityEngine.AI.NavMeshHit hit;
    private UnityEngine.AI.NavMeshPath path;
    private Vector3 center;
    public int num = 4;
    public float offset = 40f;
    public Transform target;
    public float intervalTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.autoBraking = false;
        alert = false;

        path = new UnityEngine.AI.NavMeshPath();

        center = target.position;

        ChangePoints(target.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))

        {
            Stop();
        }
    }

    public void Stop()
    {
        agent.speed = 0;
    }

    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint];

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }
    
    private void ChangePoints(Vector3 cen)
    {
        points[0] = cen + new Vector3(offset, 0, offset);
        points[1] = cen + new Vector3(-offset, 0, offset);
        points[2] = cen + new Vector3(-offset, 0, -offset);
        points[3] = cen + new Vector3(offset, 0, -offset);
    }


    public void Seek(Transform place)
    {
        alert = true;
        agent.destination = place.position;
        ChangePoints(place.position);
    }

    // Update is called once per frame
    void Update()
    {
        blocked = UnityEngine.AI.NavMesh.Raycast(transform.position, target.position, out hit, UnityEngine.AI.NavMesh.AllAreas);
        //Debug.Log(blocked);
        //Debug.Log(transform.position);

        if (!blocked)
        {
            if (Vector3.Distance(agent.destination, transform.position) > 0.1f)
            {
                UnityEngine.AI.NavMesh.CalculatePath(transform.position, target.position, UnityEngine.AI.NavMesh.AllAreas, path);
                agent.SetPath(path);
            }
        }
        else
        {
            if (alert)
            {
                if (agent.remainingDistance < 0.001f)
                {
                    alert = false;
                }
            }
            else
            {
                if (agent.remainingDistance < 0.5f)
                    GotoNextPoint();
            }

        }
    }
}
