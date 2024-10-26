using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerCard : Card
{
    public GameObject towerPrefab;
    private bool active = false;
    // Temporary until sprites
    [SerializeField] private TextMeshProUGUI text;
    private RectTransform recttransform;
    private Vector3 origin;
    private Vector3 Targetpos;

    private void Start()
    {
        recttransform = GetComponent<RectTransform>();
        origin = recttransform.position;
        Targetpos = origin;
    }

    public override void ResetCard (GameObject towerPrefab, int id)
    {
        this.towerPrefab = towerPrefab;
        this.id = id;
        isUsing = false;
        canSacrifice = true;
        Targetpos = origin;
    }

    public override bool Use()
    {
        Debug.Log("heheheha");
        if (!active)
        {
            Targetpos = origin + new Vector3(0, 30, 0);
        } else
        {
            Targetpos = origin;
        }
        active = !active;

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
        
        // moving schenanigains :)))))))))))
        Vector3 moveamt = Vector3.Lerp(recttransform.position, Targetpos, 10f * Time.deltaTime);
        recttransform.position = moveamt;

        // veer's actual card stuff
        base.Update();
        if (isUsing)
        {
            text.text = id + " USING";
        }
        else
            text.text = id+"";
    }
}
