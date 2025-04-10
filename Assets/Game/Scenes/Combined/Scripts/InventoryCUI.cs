using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryCUI : MonoBehaviour
{
    private TextMeshProUGUI bookText;
    private int counter;
    public GameObject goal;

    // Start is called before the first frame update
    void Start()
    {
        bookText = GetComponent<TextMeshProUGUI>();
        counter = 0;
    }

    public void UpdateDiamondText(InventoryC playerInventory)
    {
        bookText.text = playerInventory.NumberOfCollectibles.ToString();
        counter++;
        if (counter == 4)
        {
            goal.SetActive(true);
        }
    }
}
