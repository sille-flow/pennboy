using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedMax : MonoBehaviour
{
    public float maxSpeed = 15f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Rigidbody>().velocity.magnitude > maxSpeed)
        {
               GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * maxSpeed;
        }
    }
}
