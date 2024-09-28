using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player1movement : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 5.0f;
    private float horizontalInput;
    private Rigidbody player1Rb;
    bool facingRight = true;
    public bool isGrounded = true;

    void Start()
    {
        player1Rb = GetComponent<Rigidbody>();
        
    }

    void Update()
    {
        // Horizontal movement using left and right arrow keys
        horizontalInput = Input.GetAxis("Horizontal");



        player1Rb.velocity = new Vector3(Time.fixedDeltaTime * speed * horizontalInput * 70, player1Rb.velocity.y, player1Rb.velocity.z);

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            player1Rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PennSoccerGround"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("PennSoccerPlayer") && collision.transform.position.y + 1.43 <= transform.position.y)
        {
            isGrounded = true;
        }
    }

}