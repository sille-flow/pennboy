using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITest : MonoBehaviour
{
    [SerializeField] private TMP_Text textToEdit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textToEdit.text = $"{Time.time}";
    }
}
