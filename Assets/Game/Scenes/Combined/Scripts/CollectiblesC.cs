using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesC : MonoBehaviour
{
    public Patrol seek;
    private void OnTriggerEnter(Collider other)
    {
        InventoryC playerInventory = other.GetComponent<InventoryC>();

        if (playerInventory != null)
        {
            playerInventory.CollectiblesCollected();
            gameObject.SetActive(false);
            seek.Seek(transform);
        }
    }
}