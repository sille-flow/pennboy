using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class LuggageCart : MonoBehaviour, PushablePullableObject
{
    [SerializeField] Game level;
    public float maxSpeed = 15f;
    private float maxSqrSpeed;

    private Rigidbody cartRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        maxSqrSpeed = maxSpeed * maxSpeed;
        cartRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(cartRigidBody.velocity.sqrMagnitude > maxSqrSpeed)
        {
            cartRigidBody.velocity = cartRigidBody.velocity.normalized * maxSpeed;
        }
    }

    void OnCollisionEnter(Collision collision) {
        PropertyDamageCollider col = collision.gameObject.GetComponent<PropertyDamageCollider>();
        if (collision.gameObject.name == "Fall off zone") {
            level.reduceBudget(100000);
        }
        if (col != null) {
            int damage = col.calculateDamage(collision.impulse.magnitude);
            if (damage != 0) {
                level.reduceBudget(damage);
            }
        }
    }

    public void push(Vector3 raycastIntersectionPoint, Vector3 playerVelocity, Vector3 dir) {
        Vector3 playerVelocityInRespectToDir = Vector3.Dot(playerVelocity, dir) * dir;
        Vector3 force = playerVelocityInRespectToDir*cartRigidBody.mass + 10000*dir;
        cartRigidBody.AddForceAtPosition(force, raycastIntersectionPoint, ForceMode.Impulse);
    }

    public void pull(Vector3 raycastIntersectionPoint, Vector3 playerVelocity, Vector3 dir) {
        throw new NotImplementedException();
    }
}
