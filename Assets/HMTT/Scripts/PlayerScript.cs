using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float doubleJumpForce;
    private bool canDoubleJump;
    [SerializeField]
    private float gravityForce;
    [SerializeField]
    private float dashSpeed;
    private bool canDash;
    private Collider col;
    [SerializeField]
    private float wallJumpForce;
    private bool isWallJumping;
    private bool isAlive;
    private bool isActive;


    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
