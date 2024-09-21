using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerMain : MonoBehaviour
{

    
    [SerializeField] protected float cooldown = 1f;
    [SerializeField] protected int damage = 10;
    [SerializeField] protected float range = 7.5f;
    protected List<Vector3> enemiesInRange;
    protected float remainingCooldown = cooldown;

    // Start is called before the first frame update
    void Start()
    {

    }

    //private void onCollisionEnter(Collision collision)
    //{
    //    Debug.Log("ooga booga");
    //}

    // Update is called once per frame
    void Update()
    {
        if (remainingCooldown < cooldown) { return; }
        remainingCooldown -= cooldown;

        

        // TODO: get target enemy
        // TODO: get damage
    }
}
