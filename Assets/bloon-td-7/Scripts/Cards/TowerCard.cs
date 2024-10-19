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
        if (isUsing) return false;

        GameManager.instance.cardManager.placementSystem.enableTowerPlacement(this);
        return true;
    }

    public override void Deactivate(bool didPlace)
    {
        base.Deactivate(didPlace);
        if (!didPlace)
        {
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
