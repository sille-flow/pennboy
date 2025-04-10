using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CombinedMovement : MonoBehaviour
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
    private int jumpCount = 0;
    private bool canMove = true;
    private bool canJump = true;

    // The desired height we are transitioning to.
    private float targetHeight;

    public GameObject gameOverPanel;
    public GameObject restartButton;
    public GameObject winPanel;
    public Patrol seeker;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Seeker"))
        {
            gameOverPanel.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            canMove = false;
        }
        if (other.gameObject.CompareTag("Finish"))
        {
            winPanel.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            canMove = false;
            seeker.Stop();
        }

    }

    void Start()
    {
        gameOverPanel.gameObject.SetActive(false);
        winPanel.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        canMove = true;
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
        if (Input.GetButton("Jump") && canMove && canJump && jumpCount < 2)
        {
            moveDirection.y = jumpPower;
            canJump = false;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity when not grounded.
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;

            if (!canJump && moveDirection.y < 0 && Input.GetButton("Jump"))
            {
                Debug.Log(jumpCount);
                jumpCount++;
                canJump = true;
            }
        }

        // Crouch logic (only when grounded and not trying to jump).
        if (characterController.isGrounded)
        {
            jumpCount = 0;
            canJump = true;
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