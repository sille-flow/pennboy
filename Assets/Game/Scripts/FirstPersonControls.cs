using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    Rigidbody rb;

    public Transform playerBody;

    [Header("Movement")]
    public float speed = 3f;
    public float acceleration = 12f, decelerationFactor = 1f;
    public float mouseSensitivity = 100f;
    public bool isCrouching = false;
    public float crouchFactor = 0.5f;
    public CapsuleCollider col;

    float xRot = 0f;

    private void Start()
    {
        rb = playerBody.GetComponent<Rigidbody>();

        col = playerBody.GetComponent<CapsuleCollider>();

        //Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Look();
        Crouch();
        Walk();
    }

    public void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //rotate playerBody
        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        //Rotate the camera 
        transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);

        //Rotate the player body
        playerBody.Rotate(Vector3.up * mouseX);
    }

    void Crouch()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isCrouching = true;
            col.height = 0.5f;
        }
        else
        {
            isCrouching = false;
            col.height = 2.0f;
        }
    }


    void Walk()
    {
        Vector3 displacement;

        float maxSpeed = speed, maxAcc = acceleration;

        if (isCrouching)
        {
            maxSpeed *= crouchFactor;
            maxAcc *= crouchFactor;
        }

        displacement = playerBody.transform.forward * Input.GetAxis("Vertical") + playerBody.transform.right * Input.GetAxis("Horizontal");

        float len = displacement.magnitude;
        if (len > 0)
        {
            rb.velocity += displacement / len * Time.deltaTime * maxAcc;

            // Clamp velocity to the maximum speed.
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * speed;
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
}
