using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject mouseIndicator;
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private bool isPlacing;
    private TowerCard cardUsing;

    private void Start()
    {
        isPlacing = false;
        mouseIndicator.SetActive(false);
    }

    private void Update()
    {
        if (!isPlacing)  return;
        
        (Vector3 MousePosition, bool validplacement) = inputManager.GetPlacementPosition();

        mouseIndicator.transform.position = MousePosition + new Vector3(0, 3.5f, 0);
        if (!validplacement) { return; }

        if (Input.GetMouseButtonDown(0))
        {
            MoneyManager moneyManager = GameManager.instance.moneyManager;
            if (moneyManager.GetCurrentMoney() < 100) { return; }

            // deduct money
            moneyManager.SpendMoney(100);

            // this goes hard
            Instantiate(cardUsing.towerPrefab, MousePosition + new Vector3(0, 3.5f, 0), new Quaternion());

            disableTowerPlacement(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            disableTowerPlacement(false);
        }
    }

    /// <summary>
    /// Enables tower placement for the specified tower
    /// </summary>
    /// <param name="id">id of tower to place</param>
    /// <param name="card">reference to card calling this function</param>
    public void enableTowerPlacement(TowerCard card)
    {
        cardUsing = card;
        isPlacing = true;
        mouseIndicator.SetActive(true);
    }

    /// <summary>
    /// Disables tower placement.
    /// </summary>
    /// <param name="didPlace">True if did place the tower, false if exiting on escape</param>
    private void disableTowerPlacement(bool didPlace)
    {
        cardUsing.Deactivate(didPlace);
        isPlacing = false;
        mouseIndicator.SetActive(false);
    }
}
