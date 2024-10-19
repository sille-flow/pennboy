using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerCard : Card
{
    public GameObject towerPrefab;
    // Temporary until sprites
    [SerializeField] private TextMeshProUGUI text;

    public override void ResetCard (GameObject towerPrefab, int id)
    {
        this.towerPrefab = towerPrefab;
        this.id = id;
        isUsing = false;
    }

    public override bool Use()
    {
        if (isUsing)
        {
            GameManager.instance.cardManager.placementSystem.disableTowerPlacement(false);
            isUsing = false;
            return false;
        }

        GameManager.instance.cardManager.placementSystem.enableTowerPlacement(this);
        return true;
    }

    /// <summary>
    /// Deactivates the current tower card, either when card was used to place a tower of when card is turned off.
    /// </summary>
    /// <param name="didPlace">True if was placed, false if not placed.</param>
    public override void Deactivate(bool didPlace)
    {
        base.Deactivate(didPlace);
        if (!didPlace)
        {
            Debug.Log("Deactivated " + id);
            isUsing = false;
            return;
        }
        GameManager.instance.cardManager.removeCardFromHand(this);
    }

    protected override void Update()
    {
        base.Update();
        if (isUsing)
        {
            text.text = id + " USING";
        }
        else
            text.text = id+"";
    }
}
