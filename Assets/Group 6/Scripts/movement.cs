using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    private CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Vector3 move = gameObject.transform.right * Input.GetAxis("Horizontal") + gameObject.transform.forward * Input.GetAxis("Vertical");
        controller.Move(move);
    }
}
