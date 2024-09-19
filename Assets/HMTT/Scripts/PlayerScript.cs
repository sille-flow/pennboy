using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public Transform playerBody;

    Rigidbody rb;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float doubleJumpForce;
    private bool canDoubleJump;
    [SerializeField]
    private float gravityForce;
    [SerializeField]
    private float dashSpeed;
    private bool canDash;
    private Collider col;
    [SerializeField]
    private float wallJumpForce;
    private bool isWallJumping;
    private bool isAlive;
    private bool isActive;


    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 0;
        col = GetComponent<Collider>();
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
