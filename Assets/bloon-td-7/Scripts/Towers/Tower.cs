using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] protected float cooldown = 1f;
    [SerializeField] protected int damage = 1;
    [SerializeField] protected float range = 7.5f;
    protected List<GameObject> enemiesInRange = new List<GameObject>();
    protected float remainingCooldown;
    protected float projectileSpeed = 240f;
    protected int projectilePierce = 1;
    [SerializeField] protected int[] UpgradeCosts = { 100,200,300,400 };
    protected int level = 0;

    /// <summary>
    /// range collision detection
    /// </summary>
    /// <param name="collision"></param>
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
    protected GameObject GetTarget()
    {
        GameObject enemyMax = null;
        for (int i = 0; i < enemiesInRange.Count; i++)
        {
            if (enemiesInRange[i] == null)
            {
                enemiesInRange.RemoveAt(i);
                i--;
            }
            else
            {
                if (enemyMax == null)
                    enemyMax = enemiesInRange[i];
                else if (enemyMax.GetComponent<Enemy>().DistanceTravelled > enemiesInRange[i].GetComponent<Enemy>().DistanceTravelled)
                {
                    enemyMax = enemiesInRange[i];
                }
            }
        }
        return enemyMax;
    }


    // Start is called before the first frame update
    protected virtual void Start()
    {
        remainingCooldown = cooldown;
    }

    // Update is called once per frame
    protected virtual void Update()
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
        Attack(SelectedTarget);
    }

    protected virtual void Attack(GameObject target) { }

    /// <summary>
    /// Upgrades the tower level when given a money amount spent. Upgrade funcitonality to be implemented in children classes.
    /// </summary>
    /// <param name="cost">Money sacrificed</param>
    public void CalcLevel (int cost)
    {
        int i = 0;
        while (cost > UpgradeCosts[i])
        {
            level++;
            i++;
        }
    }

    protected virtual void Upgrade(int level)
    {

    }

    /// <summary>
    /// Returns the total cost of the tower based off of its level
    /// </summary>
    /// <returns></returns>
    public int GetCost()
    {
        int sum = 0;
        for (int i = 0; i < level; i++)
            sum += UpgradeCosts[i];
        return sum;
    }
}
