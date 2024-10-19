using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int id;
    public bool isUsing;

    private void Start()
    {
        isUsing = false;
    }

    public virtual void ResetCard(GameObject towerPrefab, int id)
    {

    }

    /// <summary>
    /// Uses the card. Returns true if successfully used it, false otherwise.
    /// </summary>
    /// <returns></returns>
    public virtual bool Use()
    {
        return false;
    }

    protected virtual void Update()
    {
        if (isUsing)
        {
            // add visual thingy here
        }
    }

    public virtual void Deactivate(bool didPlace)
    {
        
    }
}
