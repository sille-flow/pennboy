using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerMain : MonoBehaviour
{

    [SerializeField] private float cooldown = 1f;
    [SerializeField] private int damage = 10;
    private bool attackReady = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!attackReady) { return; }
        // TODO: get target enemy
        // TODO: get damage

        StartCoroutine("towerCooldown");
    }

    IEnumerator towerCooldown()
    {
        attackReady = false;
        yield return new WaitForSeconds(cooldown);
        attackReady = true;
    }
}
