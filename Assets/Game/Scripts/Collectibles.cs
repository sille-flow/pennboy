using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Inventory playerInventory = other.GetComponent<Inventory>();

        if (playerInventory != null)
        {
            playerInventory.CollectiblesCollected();
            gameObject.SetActive(false);
        }
    }
}