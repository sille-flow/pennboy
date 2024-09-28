using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text budgetText;
    private float timeStore = 0f;
    // Start is called before the first frame update
    void Start()
    {
        timeStore = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = "Time: " + $"{Mathf.Floor((Time.time - timeStore) * 100) / 100f}";
    }

    public void updateBudget(int newBudget) {
        budgetText.text = "Budget: " + $"{newBudget}";
    }
}
