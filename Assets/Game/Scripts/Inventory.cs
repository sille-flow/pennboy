using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
public class Inventory : MonoBehaviour
{
    public int NumberOfCollectibles { get; private set; }

    // initialize here so it's never null
    public UnityEvent<Inventory> OnCollectiblesCollected = new UnityEvent<Inventory>();

    public void CollectiblesCollected()
    {
        NumberOfCollectibles++;
        OnCollectiblesCollected.Invoke(this);
    }
}
