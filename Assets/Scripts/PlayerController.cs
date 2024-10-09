using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    ReadyToJump,
    JumpCharging,
    Jumping, 
    InputLocked,
}


public class PlayerController : MonoBehaviour
{
    [SerializeField] float maxHeight = 2f;
    [SerializeField] float distance = 2.5f;
    [SerializeField] float minJumpDuration = 1f;
    [SerializeField] float maxJumpDuration = 2f;
    [SerializeField] float minimumHeight = 5f;
    [SerializeField] PlatformManager platformManager; 
    [SerializeField] CameraController cameraController;

    PlayerState playerState = PlayerState.ReadyToJump;

    float jumpChargeStartTime = 0;
    float jumpChargeDuration = 0;

    Vector3 startingPos = Vector3.zero;
    Vector3 destination = Vector3.zero;
    float jumpDuration = 1;

    void Start()
    {
        float duration = minJumpDuration;
    }

    void Update()
    {
        switch(playerState)
        {
            case PlayerState.ReadyToJump:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    jumpChargeStartTime = Time.time;
                    startingPos = transform.position;
                    playerState = PlayerState.JumpCharging; 
                }
                break; 

            case PlayerState.JumpCharging:
                if (Input.GetKeyUp(KeyCode.Space) && jumpChargeStartTime != 0)
                {
                    jumpChargeDuration = Time.time - jumpChargeStartTime;
                    jumpDuration = Mathf.Clamp(jumpChargeDuration, minJumpDuration, maxJumpDuration);

                    // Add 2 directions later
                    destination = transform.position +
                        transform.forward * jumpChargeDuration * distance;

                    playerState = PlayerState.Jumping;
                }
                break; 
            case PlayerState.Jumping:
                // Jumped to Pos
                if (Vector3.Distance(transform.position, destination) < 0.05f)
                {
                    // Reset transform to upright position
                    transform.position = destination;
                    transform.rotation =
                        Quaternion.Euler(0, transform.rotation.y, transform.rotation.z);

                    // Generate new platform if jump landed 
                    platformManager.JumpLanded();
                    cameraController.MoveCamera(transform.position);

                    playerState = PlayerState.ReadyToJump; 
                }
                // Still going
                else
                {
                    float time = (Time.time - (jumpChargeStartTime + jumpChargeDuration));
                    transform.position = evaluate(startingPos, destination, time / jumpDuration);
                    transform.rotation *= Quaternion.Euler((Time.deltaTime / jumpDuration) * 360, 0, 0);
                }
                break; 
        }
    }

    public Vector3 evaluate(Vector3 startPos, Vector3 endPos, float t)
    {
        Vector3 midPos = (endPos + startPos)/2;
        midPos.y = Vector3.Distance(startPos, endPos) * maxHeight;
        if (midPos.y < minimumHeight) midPos.y = minimumHeight;
        Vector3 ab = Vector3.Lerp(startPos, midPos, t);
        Vector3 bc = Vector3.Lerp(midPos, endPos, t);
        return Vector3.Lerp(ab, bc, t);
    }

    private void OnDrawGizmos()
    {
        if (startingPos == null || destination == null) return;
        for (int i = 0; i < 20; i++)
        {
            Gizmos.DrawWireSphere(evaluate(startingPos,destination, i / 20f), 0.1f);
        }
    }
}
