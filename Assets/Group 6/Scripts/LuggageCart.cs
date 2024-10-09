using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class LuggageCart : MonoBehaviour
{
    [SerializeField] Game level;
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

    void OnCollisionEnter(Collision collision) {
        PropertyDamageCollider col = collision.gameObject.GetComponent<PropertyDamageCollider>();
        if (col != null) {
            int damage = col.calculateDamage(collision.impulse.magnitude);
            if (damage != 0) {
                level.reduceBudget(damage);
            }
        }
    }
}
