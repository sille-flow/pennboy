using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed { get; set; }
    [SerializeField]
    private float maxSpeed { get; set; }
    [SerializeField]
    private float jumpForce { get; set; }
    [SerializeField]
    private float doubleJumpForce { get; set; }
    private bool canDoubleJump;
    [SerializeField]
    private float gravityForce { get; set; }
    [SerializeField]
    private float dashSpeed { get; set; }
    private bool isDashing { get; }
    private bool canDash { get; set; }
    private float dashCD { get; set; }
    private Collider col;
    private Rigidbody rb;
    [SerializeField]
    private float wallJumpForce { get; set; }
    private bool isOnWall { get; }
    private bool isWallJumping;
    private bool isAlive { get; set; }
    private bool isActive { get; set; }

    public void addForce(Vector3 force)
    {
        rb.AddForce(force);
    }

    public Vector3 getVelocity()
    {
        return rb.velocity;
    }

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
