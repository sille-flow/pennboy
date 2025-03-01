using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public int NumberOfCollectibles { get; private set; }

    public UnityEvent<Inventory> OnCollectiblesCollected;

    public void CollectiblesCollected()
    {
        NumberOfCollectibles++;
        OnCollectiblesCollected.Invoke(this);
    }
}
