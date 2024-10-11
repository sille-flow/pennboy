using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public float waveCooldown = 5f;
    public float timer = 0;
    public bool waveOccurring = false;
    public GameObject waveList;
    public int waveIndex = 0;
    [SerializeField] private TextMeshProUGUI roundText;

    // Start is called before the first frame update
    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        roundText.text = "Round:\n" + waveIndex;
        if (timer < waveCooldown && !waveOccurring)
        {
            timer += Time.deltaTime;
        }
        else
        {
            if (!waveOccurring)
            {
                Debug.Log(waveIndex);
                Instantiate(waveList.transform.GetChild(waveIndex).gameObject, transform.position, transform.rotation);
                waveOccurring = true;
                waveIndex++;
                timer = 0;
            }
        }

    }
}
