using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wave2 : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public GameObject enemyList;

    private int enemyIndex = 0;
    public int enemiesSpawned = 0;
    private float spawnRate = 0.25f;
    private int enemyCount = 20;
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
        else if (enemiesSpawned < enemyCount)
        {
            timer = 0;
            enemiesSpawned += 1;
            
            switch (enemiesSpawned)
            {
                case 15:
                    enemyIndex = 1;
                    break;
                case 10:
                    enemyIndex = 2;
                    break;
            }

            //Debug.Log("enemiesSpanwed: " + enemiesSpawned + "enemyIndex: " + enemyIndex);
            GameObject newEnemy = Instantiate(enemyList.transform.GetChild(enemyIndex).gameObject, transform.position, transform.rotation);
            enemies.Add(newEnemy);
            if (checkAllNull(enemies) && enemies.Count == enemyCount)
            {
                //Debug.Log("start next wave countdown");
                GameManager.instance.waveManager.waveOccurring = false;
            }
        }
    }
    private bool checkAllNull(List<GameObject> l)
    {
        foreach (GameObject obj in l)
        {
            if (obj != null)
            {
                return false;
            }
        }
        return true;
    }
}
