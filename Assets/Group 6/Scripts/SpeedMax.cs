using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedMax : MonoBehaviour
{
    public float maxSpeed = 15f;
    private float maxSqrSpeed;
    // Start is called before the first frame update
    void Start()
    {
        maxSqrSpeed = maxSpeed * maxSpeed;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(GetComponent<Rigidbody>().velocity.sqrMagnitude > maxSqrSpeed)
        {
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * maxSpeed;
        }
    }
}
