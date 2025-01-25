using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

namespace group13 {
public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;
    //public float speed = 0.1f;

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * 5 * Time.deltaTime);
        
    }
}
}