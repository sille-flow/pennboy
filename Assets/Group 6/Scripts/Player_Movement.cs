using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private CharacterController controller;

    private Vector3 playerVelocity = new Vector3(0,0,0);
    private bool groundedPlayer;
    private float basePlayerSpeed = 5.0f;

    private float playerSpeed = 5.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    private float jumpVelocity;

    private float playerMass = 120;

    private float mouseSensitivity = 1;

    private float speedUp = 2f;

    private void Start()
    {
        jumpVelocity = Mathf.Sqrt(-2 * gravityValue * jumpHeight);
        controller = gameObject.GetComponent<CharacterController>();
        // set the skin width appropriately according to Unity documentation: https://docs.unity3d.com/Manual/class-CharacterController.html
        controller.skinWidth = 0.1f * controller.radius;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += jumpVelocity;
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        playerVelocity.x = 0;
        playerVelocity.z = 0;
        if (Input.GetKey(KeyCode.LeftShift)) {
            playerSpeed = basePlayerSpeed * speedUp;
        } else {
            playerSpeed = basePlayerSpeed;
        }
        playerVelocity += (gameObject.transform.right * Input.GetAxis("Horizontal") + gameObject.transform.forward * Input.GetAxis("Vertical")) * playerSpeed;
        controller.Move(playerVelocity * Time.deltaTime);

        
        // Rotates the camera
        float rotX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float rotY = -Input.GetAxis("Mouse Y") * mouseSensitivity;
        gameObject.transform.Rotate(0, rotX, 0);
        Camera.main.transform.Rotate(rotY, 0, 0);
        if (Camera.main.transform.localEulerAngles.y == 180 && Camera.main.transform.localEulerAngles.z == 180) {
            Camera.main.transform.localEulerAngles = new Vector3(Camera.main.transform.localEulerAngles.x, 0, 0);
        }
        
        //Camera.main.transform.localEulerAngles = cameraRotationVector;
        gameObject.transform.Rotate(0, rotX, 0);
    }

    void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.rigidbody != null) {
            Vector3 horizontalDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
            hit.rigidbody.AddForce(horizontalDir * 100000);
        }
    }
}
