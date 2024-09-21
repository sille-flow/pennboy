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
    private float dashDuration { get; set; }
    private Collider col;
    private Rigidbody rb;

    private CharacterController cha;
    [SerializeField]
    private float wallJumpForce { get; set; }
    private bool isOnWall { get; set; }
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
        canDash = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfGrounded();
        CheckWall();
        Move();
        Jump();
        DoubleJump();
        CheckDash();
        WallJump();
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

        if(Input.GetKeyDown(KeyCode.W)&&isGrounded&&!isOnWall)
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

    public void WallJump()
    {
        if(Input.GetKeyDown(KeyCode.W)&&isOnWall)
        {
            wallJumpForce = 20f*gravityForce;
            Vector3 jumpDirection;
            if (isWallRight)
            {
                moveSpeed = -3f;
                jumpDirection = new Vector3(-0.2f, 5f, 0); 
                Debug.Log("Wall Jump Triggered - Right Wall");
            }
            else if (isWallLeft)
            {
                moveSpeed = 3f;
                jumpDirection = new Vector3(0.2f, 5f, 0);
                Debug.Log("Wall Jump Triggered - Left Wall");
            } else {
                jumpDirection = Vector3.up;
            }
            isWallJumping = true;
            rb.transform.Translate(jumpDirection * wallJumpForce * Time.deltaTime);
            }
    }

    public void CheckDash()
    {
        dashCD = 2f;

        if (canDash && Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
        {
            StartCoroutine(Dash());
        }

        if (canDash && Input.GetKeyDown(KeyCode.LeftShift) && !isGrounded)
        {
            StartCoroutine(AirDash());
        }
    }

    public IEnumerator Dash()
    {
        dashSpeed = 30f;
        canDash = false;
        isDashing = true;
        Debug.Log("Dash Triggered");

        dashDuration = 0.15f;
        float dashCounter = 0f;

        while (dashCounter < dashDuration)
        {
            CheckWall();
            if (isWallRight) {
                dashSpeed = 0;
            }
            rb.transform.Translate(playerBody.transform.right*dashSpeed*Time.deltaTime);
            dashCounter += Time.deltaTime;
            yield return null;
        }

        isDashing = false;

        yield return new WaitForSeconds(dashCD);
        canDash = true;
    }

    public IEnumerator AirDash()
    {
        dashSpeed = 20f;
        canDash = false;
        isDashing = true;
        Debug.Log("Air Dash Triggered");

        dashDuration = 0.1f;
        float dashCounter = 0f;

        while (dashCounter < dashDuration)
        {
            CheckWall();
            if (isWallRight) {
                dashSpeed = 0;
            }
            rb.transform.Translate(playerBody.transform.right*dashSpeed*Time.deltaTime);
            dashCounter += Time.deltaTime;
            yield return null;
        }

        isDashing = false;

        yield return new WaitForSeconds(dashCD);
        canDash = true;
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

        isOnWall = isWallLeft || isWallRight; 
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
