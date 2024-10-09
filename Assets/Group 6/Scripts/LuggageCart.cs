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
    [SerializeField] private float damageSlope = 1;
    [SerializeField] private float damageOffset = 0;
    [SerializeField] private float maxDamage = 100;
    [SerializeField] private float cooldownTime;
    [SerializeField] private AudioSource crashSound;
    private float currCooldownTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        maxSqrSpeed = maxSpeed * maxSpeed;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (currCooldownTime > 0) {
            currCooldownTime = Mathf.Clamp(currCooldownTime - Time.deltaTime, 0, cooldownTime);
        }
        if(GetComponent<Rigidbody>().velocity.sqrMagnitude > maxSqrSpeed)
        {
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * maxSpeed;
        }
    }

    void OnCollisionEnter(Collision collision) {
        int damage = 0;
        PropertyDamageCollider col = collision.gameObject.GetComponent<PropertyDamageCollider>();
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Group6Wall") {
            crashSound.Play(0);
            damage = Mathf.RoundToInt(Mathf.Clamp(damageSlope * collision.impulse.magnitude + damageOffset, 0, maxDamage));
            Debug.Log(damage);
            if (damage > 0 && currCooldownTime == 0) {
                currCooldownTime = cooldownTime;
                level.reduceBudget(damage);
            }
        } else if (col != null) {
            damage = col.calculateDamage(collision.impulse.magnitude);
            if (damage != 0) {
                level.reduceBudget(damage);
            }
        }
    }
}
