using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2movement : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 5.0f;
    private float horizontalInput;
    private Rigidbody player2Rb;
    public bool isGrounded = true;

    void Start()
    {
        player2Rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Horizontal movement using wasd
        horizontalInput = Input.GetAxis("Horizontal2");
        player2Rb.velocity = new Vector3(Time.fixedDeltaTime * speed * horizontalInput*70, player2Rb.velocity.y, player2Rb.velocity.z);
        
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            player2Rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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
