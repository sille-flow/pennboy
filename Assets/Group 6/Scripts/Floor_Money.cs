using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor_Money : MonoBehaviour
{
    [SerializeField] Game level;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0.5f, 0);
    }

    void OnCollisionEnter(Collision collision) {
        PropertyDamageCollider col = collision.gameObject.GetComponent<PropertyDamageCollider>();
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Player") {
            Debug.Log("Collide with player");
            level.reduceBudget(-100);
            Debug.Log("Attempted to add");
            Destroy(gameObject);
        }
    }
}
