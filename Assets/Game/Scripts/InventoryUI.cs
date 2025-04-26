using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [Tooltip("Drag your Player (with Inventory) here in the Inspector)")]
    [SerializeField] private Inventory playerInventory;

    private TextMeshProUGUI bookText;

    void Start()
    {
        bookText = GetComponent<TextMeshProUGUI>();

        if (playerInventory != null)
        {
            // Subscribe so UI updates on every collect
            playerInventory.OnCollectiblesCollected.AddListener(UpdateDiamondText);
            // Initialize display
            UpdateDiamondText(playerInventory);
        }
    }

    private void UpdateDiamondText(Inventory inv)
    {
        bookText.text = inv.NumberOfCollectibles.ToString();
    }
}
