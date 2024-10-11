using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class TowerMain : MonoBehaviour
{

    
    [SerializeField] protected float cooldown = 1f;
    [SerializeField] protected int damage = 1;
    [SerializeField] protected float range = 7.5f;
    public List<GameObject> enemiesInRange = new List<GameObject>();
    protected float remainingCooldown;
    protected float projectileSpeed = 120f;
    protected int projectilePierce = 1;

    // Start is called before the first frame update
    void Start()
    {
        remainingCooldown = cooldown;
    }

    // range collision detection
    private void OnCollisionEnter(Collision collision)
    {
        GameObject hit = collision.gameObject;
        if (hit.tag == "BTD7Enemy")
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
            if (target == null)
            {
                enemiesInRange.Remove(target);
                return GetTarget();
            }
            return target;
        };
        return null;

        /* ATTEMPT AT ENEMY TARGETING
        GameObject enemyMax = null;
        foreach (var target in enemiesInRange)
        {
            if (target == null)
                enemiesInRange.Remove(target);
            else
            {
                if (enemyMax == null)
                    enemyMax = target;
                else if (enemyMax.GetComponent<Enemy>().DistanceTravelled < target.GetComponent<Enemy>().DistanceTravelled)
                {
                    enemyMax = target;
                }
            }
        }
        return enemyMax;
        */
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
        //SelectedTarget.GetComponent<Enemy>().Damage(damage);
        Attack(SelectedTarget);


        // TODO: get target enemy
        // TODO: get damage
    }

    protected void Attack(GameObject target)
    {
        if (target == null) return;
        Projectile projectile = Instantiate(GameManager.instance.projectile, transform.position, transform.rotation).GetComponent<Projectile>();
        projectile.Initialize(damage, projectileSpeed, projectilePierce, target.transform.position);
    }
}
