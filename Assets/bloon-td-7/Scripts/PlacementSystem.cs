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
    private Object toBePlaced;

    private void Update()
    {
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
            Instantiate(toBePlaced, MousePosition + new Vector3(0, 3.5f, 0), new Quaternion());
        }
    }
}
