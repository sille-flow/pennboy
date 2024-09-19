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
        maxSpeed = 5;
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
        if(Input.GetKey(KeyCode.D))
        {
            if(moveSpeed < maxSpeed){
                moveSpeed += .08f;
            }
        }
        else{
            if(moveSpeed>0){
                moveSpeed -= .045f;
            }
            if(moveSpeed>-.01 && moveSpeed < .01) moveSpeed = 0;
        }
        if(Input.GetKey(KeyCode.A))
        {
            if(moveSpeed > -maxSpeed){
                moveSpeed -= .08f;
            }
        }
        else{
            if(moveSpeed<0){
                moveSpeed += .045f;
            }
            if(moveSpeed>-.01 && moveSpeed < .01) moveSpeed = 0;
        }
        rb.transform.Translate(playerBody.transform.right*moveSpeed*Time.deltaTime);


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
