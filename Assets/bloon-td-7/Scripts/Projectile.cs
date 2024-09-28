using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple;

public class Projectile : MonoBehaviour
{
    private int dmg=0;
    private float speed=0;
    private Rigidbody rb;
    private int pierce_cap=0;
    private int pierce_counter=0;
    public void Initialize(int dmg, float speed, int pierce, Vector3 target)
    {
        this.dmg = dmg;
        this.speed = speed;
        pierce_cap = pierce;
        pierce_counter = 0;
        rb = GetComponent<Rigidbody>();
        Vector3 rotation = target-transform.position;
        Vector3 direction = transform.position - target;
        rb.velocity = new Vector3(direction.x, transform.position.y, direction.y).normalized*speed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Enemy") return;

        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        enemy.Damage(dmg);
        pierce_counter++;
        if (pierce_counter >= pierce_cap) Destroy(gameObject);
    }




}
