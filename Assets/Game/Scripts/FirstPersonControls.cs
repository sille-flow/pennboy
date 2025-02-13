using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
	public Camera playerCamera;
	public float walkSpeed = 6f;
	public float runSpeed = 12f;
	public float jumpPower = 5f;
	public float gravity = 20f;
	public float lookSpeed = 2f;
	public float lookXLimit = 45f;
	public float defaultHeight = 2f;
	public float crouchHeight = 1f;
	public float crouchSpeed = 3f;

	// Speed of the smooth transition for character height
	public float heightTransitionSpeed = 10f;

	private Vector3 moveDirection = Vector3.zero;
	private float rotationX = 0f;
	private CharacterController characterController;
	private bool canMove = true;

	// The desired height we are transitioning to.
	private float targetHeight;

	void Start()
	{
		characterController = GetComponent<CharacterController>();
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		targetHeight = defaultHeight;
	}

	void Update()
	{
		// Determine movement directions relative to player orientation.
		Vector3 forward = transform.TransformDirection(Vector3.forward);
		Vector3 right = transform.TransformDirection(Vector3.right);

		bool isRunning = Input.GetKey(KeyCode.LeftShift);
		float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
		float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
		float movementDirectionY = moveDirection.y;
		moveDirection = (forward * curSpeedX) + (right * curSpeedY);

		// Jumping
		if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
		{
			moveDirection.y = jumpPower;
		}
		else
		{
			moveDirection.y = movementDirectionY;
		}

		// Apply gravity when not grounded.
		if (!characterController.isGrounded)
		{
			moveDirection.y -= gravity * Time.deltaTime;
		}

		// Crouch logic (only when grounded and not trying to jump).
		if (characterController.isGrounded)
		{
			if (Input.GetKey(KeyCode.LeftControl) && canMove && !Input.GetButton("Jump"))
			{
				targetHeight = crouchHeight;
				walkSpeed = crouchSpeed;
				runSpeed = crouchSpeed;
			}
			else
			{
				targetHeight = defaultHeight;
				walkSpeed = 6f;
				runSpeed = 12f;
			}
		}
		else
		{
			// Ensure the player returns to the standing height in the air.
			targetHeight = defaultHeight;
			walkSpeed = 6f;
			runSpeed = 12f;
		}

		// Smoothly interpolate the character controller's height to the target height.
		characterController.height = Mathf.Lerp(characterController.height, targetHeight, Time.deltaTime * heightTransitionSpeed);

		// Move the character.
		characterController.Move(moveDirection * Time.deltaTime);

		// Mouse look functionality.
		if (canMove)
		{
			rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
			rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
			playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
			transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
		}
	}
}


/*
public class NewBehaviourScript : MonoBehaviour
{
    Rigidbody rb;

    public Transform playerBody;

    [Header("Movement")]
    public float speed = 3f;
    public float acceleration = 12f, decelerationFactor = 1f;
    public float mouseSensitivity = 50f;
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
*/