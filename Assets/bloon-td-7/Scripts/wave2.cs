using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wave2 : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public GameObject enemyList;

    private int enemyIndex = 0;
    public int enemiesSpawned;
    private float spawnRate = 0.25f;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            enemiesSpawned += 1;
            switch (enemiesSpawned)
            {
                case 20:
                    Destroy(gameObject);
                    break;
                case 15:
                    enemyIndex = 1;
                    break;
                case 10:
                    enemyIndex = 2;
                    break;
            }
            //Debug.Log("enemiesSpanwed: " + enemiesSpawned + "enemyIndex: " + enemyIndex);
            Instantiate(enemyList.transform.GetChild(enemyIndex).gameObject, transform.position, transform.rotation);
        }
    }
}
