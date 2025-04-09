using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerScript : MonoBehaviour
{
    public Transform target;
    public float runSpeed = 0.5f;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        moveDirection = (target.position - transform.position).normalized;
        Debug.Log(moveDirection);
        float currSpeedX = runSpeed * moveDirection.x;
        float currSpeedZ = runSpeed * moveDirection.z;
        moveDirection = (right *  currSpeedX) + (forward * currSpeedZ);

        characterController.Move(moveDirection * Time.deltaTime);

    }
}
