using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testWave : MonoBehaviour
{
    public GameObject enemy;
    public GameObject enemy2;
    private List<GameObject> enemies = new List<GameObject>();
    
    private int enemyIndex = 0;
    public int enemiesSpawned;
    private float spawnRate = 2;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        enemies.Add(enemy);
        enemies.Add(enemy2);
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
            if (enemiesSpawned > 3) {
                enemyIndex = 1;
            }
            Instantiate(enemies[enemyIndex], transform.position, transform.rotation);
        }
    }
}
