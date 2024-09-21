using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
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
        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        cha = GetComponent<CharacterController>();
        canDash = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void applyGravity() //1
    {
        //apply force in the direction of gravity
    }

    void Move() //2
    {
        //move function should move the player using rb.AddForce rather than transform.Translate
        //can use Input.GetAxis("Horizontal")
    }

    void Jump() //3
    {
        //use AddForce()
    }

    void DoubleJump() //4
    {

    }

    void WallJump() //3
    {

    }

    void CheckDash() //2
    {

    }

    IEnumerator Dash() //3
    {
        //the dash should set the player's velocity to the dash velocity IF the player's original velocity was slower than the dash's velocity.
        //otherwise, use the player's original velocity
        //goign to need to store the player's original velocity in a temporary variable
        yield return new WaitForSeconds(1);
    }

    IEnumerator AirDash() //3
    {
        yield return new WaitForSeconds(1);
    }

    void CheckWall() //3
    {
        //when the player is stuck to the wall, they should slowly slide down
        //change gravityForce
    }

    void CheckIfGround() //2
    {

    }
}
