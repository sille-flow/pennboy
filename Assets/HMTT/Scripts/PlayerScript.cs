using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public bool isGrounded;
    private bool isWallRight;
    private bool isWallLeft;
    [SerializeField]
    private float dashSpeed { get; set; }
    private bool isDashing { get; set; }
    private bool canDash { get; set; }
    private float dashCD { get; set; }
    private Collider col;
    private Rigidbody rb;

    private CharacterController cha;
    [SerializeField]
    private float wallJumpForce { get; set; }
    private bool isOnWall { get; }
    private bool isWallJumping;
    private bool isAlive { get; set; }
    private bool isActive { get; set; }

    private Vector3 spherePos;
    private Vector3 offsetHeight;

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
        maxSpeed = 8f;
        moveSpeed = 0f;
        gravityForce = 5f; //Temporary gravity for testing
        jumpForce = 0f;
        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        cha = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfGrounded();
        CheckWall();
        Move();
        Jump();
        DoubleJump();
    }
    public void Move()
    {
        if(Input.GetKey(KeyCode.D))
        {
            if(moveSpeed < maxSpeed){
                moveSpeed += .06f;
                CheckWall();
            }
            if(isWallRight) moveSpeed = 0;
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
                CheckWall();
                moveSpeed -= .08f;
            }
            if(isWallLeft) moveSpeed = 0;
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
        if(!isGrounded){
            if(jumpForce > -15){
                jumpForce += -.05f*gravityForce;
            }
        }

        if(isGrounded){
            jumpForce = 0;
        }

        if(Input.GetKeyDown(KeyCode.W)&&isGrounded)
        {
            jumpForce = 2.6f*gravityForce;
            canDoubleJump = true;
        }
        
        if(jumpForce>-.01 && jumpForce < .01) jumpForce = 0;

        rb.transform.Translate(playerBody.transform.up*jumpForce*Time.deltaTime);
    }

    public void DoubleJump()
    {
        if(canDoubleJump&&Input.GetKeyDown(KeyCode.W)&&!isGrounded){
            jumpForce = 2f*gravityForce;
            canDoubleJump = false;
        }
    }

    public IEnumerator Dash()
    {
        dashCD = 1f;
        dashSpeed = 10f;
        if(canDash&&Input.GetKeyDown(KeyCode.LeftShift)&&isGrounded) {
            canDash = false;
            isDashing = true;
            gravityForce = 0f;

            yield return new WaitForSeconds(0.5f);
            isDashing = false;
            gravityForce = 5f; //back to original gravity
            
            yield return new WaitForSeconds(dashCD);
            canDash = true;

        }

    }

    public void AirDash()
    {

    }

    private void CheckWall()
    {
        RaycastHit hit; //get a hit variable to store the hit information
       
        spherePos = transform.position;
        if (Physics.Raycast(spherePos, -transform.right, out hit, .2f)){
            isWallRight = false;
            isWallLeft = true;
        }
        else if(Physics.Raycast(spherePos, transform.right, out hit, .2f)){
            isWallLeft = false;
            isWallRight= true;
        }
        else{
            isWallRight = false;
            isWallLeft = false;
        }
        
    }
    private void CheckIfGrounded()
    {
        RaycastHit hit; //get a hit variable to store the hit information
       
        spherePos = transform.position - offsetHeight;
        if (Physics.SphereCast(spherePos, .4f,  -transform.up, out hit, .11f))
        {
            isGrounded = true;
        } else {
            isGrounded = false;   
        }
    }
}
