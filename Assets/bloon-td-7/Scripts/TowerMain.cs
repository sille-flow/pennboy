using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerMain : MonoBehaviour
{

    
    [SerializeField] protected float cooldown = 1f;
    [SerializeField] protected int damage = 1;
    [SerializeField] protected float range = 7.5f;
    protected List<GameObject> enemiesInRange = new List<GameObject>();
    protected float remainingCooldown;

    // Start is called before the first frame update
    void Start()
    {
        remainingCooldown = cooldown;
    }

    // range collision detection
    private void OnCollisionEnter(Collision collision)
    {
        GameObject hit = collision.gameObject;
        if (hit.tag == "Enemy")
        {
            enemiesInRange.Add(hit);
        }
            
    }

    private void OnCollisionExit(Collision collision)
    {
        enemiesInRange.Remove(collision.gameObject);
    }

    /// <summary>
    /// Gets the target from enemies in a towers range.
    /// </summary>
    private GameObject GetTarget()
    {
        foreach (var target in enemiesInRange)
        {
            return target;
        };
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingCooldown < cooldown)
        {
            remainingCooldown += Time.deltaTime;
            return;
        }

        // get the target enemy
        GameObject SelectedTarget = GetTarget();
        if (SelectedTarget == null) { return; }

        // reset cooldown
        remainingCooldown = 0;

        // attack
        Debug.Log("Pew");
        SelectedTarget.GetComponent<Enemy>().Damage(damage);


        // TODO: get target enemy
        // TODO: get damage
    }
}
