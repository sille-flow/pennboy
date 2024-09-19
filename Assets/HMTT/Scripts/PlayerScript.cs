using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public Transform playerBody;

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
        moveSpeed = 0;
        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        Vector3 displacement;
        float maxAcc = 10f;
        float decelerationFactor = 1f;
        float acceleration = 10f;
        while(moveSpeed < 10)
        {
            if(Input.GetKey(KeyCode.D))
            {
                moveSpeed++;
            }
        }
        displacement = playerBody.transform.right * moveSpeed;

        float len = displacement.magnitude;
        if (len > 0)
        {
            rb.velocity += displacement / len * Time.deltaTime * maxAcc;

            // Clamp velocity to the maximum speed.
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * moveSpeed;
            }
        }
        else
        {
            // If no buttons are pressed, decelerate.
            len = rb.velocity.magnitude;
            float decel = acceleration * decelerationFactor * Time.deltaTime;
            if (len < decel) rb.velocity = Vector3.zero;
            else
            {
                rb.velocity -= rb.velocity.normalized * decel;
            }
        }
    }

    public void Jump()
    {

    }

    public void DoubleJump()
    {

    }

    public void Dash()
    {

    }

    public void AirDash()
    {

    }
}
