using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITest : MonoBehaviour
{
    [SerializeField] private TMP_Text textToEdit;
    private float timeStore = 0f;
    // Start is called before the first frame update
    void Start()
    {
        timeStore = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        textToEdit.text = "Time: " + $"{Mathf.Floor((Time.time - timeStore) * 100) / 100f}";
    }
}
