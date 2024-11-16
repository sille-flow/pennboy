using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class TowerMain : Tower
{
    [SerializeField] protected float projectileSpeed = 240f;
    protected int projectilePierce = 1;
    protected override void Attack(GameObject target)
    {
        if (target == null) return;
        Projectile projectile = Instantiate(GameManager.instance.projectile, transform.position, transform.rotation).GetComponent<Projectile>();
        projectile.Initialize(damage, projectileSpeed, projectilePierce, target.transform.position);
    }

    protected override void Upgrade(int level)
    {
        switch (level)
        {
            case 0:
                return;
            default:
                Debug.Log("Upgraded to level " + level);
                return;
        }
    }
}
