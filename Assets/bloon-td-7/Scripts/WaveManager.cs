using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public float waveCooldown = 5f;
    public float timer = 0;
    public bool waveOccurring = false;
    public GameObject waveList;
    public int waveIndex = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer < waveCooldown && !waveOccurring)
        {
            timer += Time.deltaTime;
        }
        else
        {
            if (!waveOccurring)
            {
                Instantiate(waveList.transform.GetChild(waveIndex).gameObject, transform.position, transform.rotation);
                waveOccurring = true;
                timer = 0;
            }
        }

    }
}
