using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 2f;
    public int waypointIndex = 0;
    protected List<Vector3> waypoints;
    protected Rigidbody rb;
    protected int dmg = 1;
    protected int health = 1;
    protected int id;
    protected int moneyWorth;
    protected string modifiers;
    private const float WAYPOINT_CHANGE_DISTANCE = 0.1f;
    public bool isCamo { get; protected set; }
    private bool canStart = false;

    private void Start()
    {
        Initialize(0.1f, 1, 1, 0, 1, false);
    }

    public void Initialize(float moveSpeed, int dmg, int health, int id, int moneyWorth, bool isCamo)
    {
        this.health = health;
        this.id = id;
        this.dmg = dmg;
        this.moveSpeed = moveSpeed;
        this.isCamo = isCamo;
        this.moneyWorth = moneyWorth;
        waypoints = new List<Vector3>();

        Transform waypointListTransform = GameObject.Find("EnemyWaypoints").transform;
        // Temp script to get all waypoints until game manager is implemented
        foreach (Transform child in waypointListTransform)
        {
            waypoints.Add(child.position);
        }

        transform.position = waypoints[0];

        rb = GetComponent<Rigidbody>();

        canStart = true;
    }

    protected void Update()
    {
        if (canStart)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex + 1], moveSpeed);
            if (CalcDistance(transform.position, waypoints[waypointIndex+1]) <= WAYPOINT_CHANGE_DISTANCE)
            {
                Debug.Log("Changed to Waypoint " + (waypointIndex + 1));
                transform.position = waypoints[waypointIndex + 1];
                waypointIndex++;
                if (waypointIndex >= waypoints.Count-1)
                {
                    Debug.Log("Reached the End");
                    Destroy(gameObject);
                }
            }
        }
    }

    /// <summary>
    /// Returns the distance between the enemy and its next waypoint.
    /// </summary>
    /// <returns>float distance to next waypoint</returns>
    public float GetDistanceToWaypoint()
    {
        return CalcDistance(transform.position, waypoints[waypointIndex + 1]);
    }

    private float CalcDistance(Vector3 pos1, Vector3 pos2)
    {
        float dx = pos2.x - pos1.x;
        float dy = pos2.y - pos1.y;
        float dz = pos2.z - pos1.z;

        return Mathf.Sqrt((dx*dx)+(dy*dy)+(dz*dz));
    }
}
