using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowRealmPortalScript : MonoBehaviour
{
    //This is the script for the shadow realm portal obstacle. When the player makes contact with this obstacle, gravity should be reversed

    public float antiGravityForce = -10.0f;
    private MovementScript player;

    // Start is called before the first frame update
    void Start()
    {
        antiGravityForce = -10.0f;
        player = FindObjectOfType<MovementScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            MovementScript playerMovement = other.GetComponent<MovementScript>();
            playerMovement.SetGravity(antiGravityForce);
        }
    }   

}
