using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyDamageCollider : MonoBehaviour
{
    [SerializeField]
    private float damageSlope = 1;
    [SerializeField]
    private float damageOffset = 0;
    [SerializeField]
    private float maxDamage = 100;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private float calculateDamage(float normalSpeed) {
        return Mathf.Clamp(damageSlope * normalSpeed + damageOffset, 0, maxDamage);
    }


}
