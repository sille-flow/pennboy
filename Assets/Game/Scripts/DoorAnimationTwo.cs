using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorAnimationTwo : MonoBehaviour
{
    private Animator _doorAnimator;

    void Start()
    {
        _doorAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // Get the player's inventory
        Inventory playerInventory = other.GetComponent<Inventory>();
        if (playerInventory != null && playerInventory.NumberOfCollectibles > 3)
        {
            _doorAnimator.SetTrigger("Open");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // Only close if the door was open (i.e. they still have ≥4)
        Inventory playerInventory = other.GetComponent<Inventory>();
        if (playerInventory != null && playerInventory.NumberOfCollectibles > 3)
        {
            _doorAnimator.SetTrigger("Closed");
        }
    }
}