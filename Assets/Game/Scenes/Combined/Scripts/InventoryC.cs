using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryC : MonoBehaviour
{
    public int NumberOfCollectibles { get; private set; }

    public UnityEvent<InventoryC> OnCollectiblesCollected;

    public void CollectiblesCollected()
    {
        NumberOfCollectibles++;
        OnCollectiblesCollected.Invoke(this);
    }
}
