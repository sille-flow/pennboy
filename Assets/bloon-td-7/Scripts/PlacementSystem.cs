using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject mouseIndicator;
    [SerializeField]
    private InputManager inputManager;

    private void Update()
    {
        Vector3 MousePosition = inputManager.GetPlacementPosition();
        mouseIndicator.transform.position = MousePosition;
    }
}
