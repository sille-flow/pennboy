using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform elevatorPlatform; // The elevator object
    public Transform targetPosition;   // The position where the elevator moves up to
    public Transform startPosition;    // The position where the elevator moves down to (starting position)
    public float elevatorSpeed = 2.0f; // Speed of the elevator movement
    public float delayTime = 2.0f;     // Delay before the elevator moves back down

    
    private bool playerInTrigger = false;  // To track if player is in the trigger area
    private bool isMoving = false;         // To check if the elevator is already moving

    // Detect when the player enters the trigger area
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && !isMoving)
        {
            playerInTrigger = true;
            StartCoroutine(MoveElevatorUp());
        }
    }

    // Detect when the player exits the trigger area
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            playerInTrigger = false;
        }
    }

    // Coroutine to move the elevator up
    IEnumerator MoveElevatorUp()
    {
        isMoving = true;

        // Move the elevator to the target position
        while (Vector3.Distance(elevatorPlatform.position, targetPosition.position) > 0.5f)
        {
            elevatorPlatform.position = Vector3.MoveTowards(elevatorPlatform.position, targetPosition.position, elevatorSpeed * Time.deltaTime);
            yield return null;
        }

        // Wait for a delay before moving down
        yield return new WaitForSeconds(delayTime);

        // If the player has exited the trigger area, move the elevator back down
        if (!playerInTrigger)
        {
            StartCoroutine(MoveElevatorDown());
        }
        else
        {
            isMoving = false; // Allow the elevator to move again if the player triggers it again
        }
    }

    // Coroutine to move the elevator down
    IEnumerator MoveElevatorDown()
    {
        isMoving = true;

        // Move the elevator to the start position
        while (Vector3.Distance(elevatorPlatform.position, startPosition.position) > 0.5f)
        {
            elevatorPlatform.position = Vector3.MoveTowards(elevatorPlatform.position, startPosition.position, elevatorSpeed * Time.deltaTime);
            yield return null;
        }

        isMoving = false; // The elevator is now ready to be triggered again
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
