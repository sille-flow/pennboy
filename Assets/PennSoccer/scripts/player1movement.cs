using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player1movement : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 5.0f;
    private float horizontalInput;
    private Rigidbody player1Rb;
    public bool isGrounded = true;

    void Start()
    {
        player1Rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Horizontal movement using left and right arrow keys
        horizontalInput = Input.GetAxis("Horizontal");



        player1Rb.velocity = new Vector3(Time.deltaTime * speed * horizontalInput * 700, player1Rb.velocity.y, player1Rb.velocity.z);

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            player1Rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

}