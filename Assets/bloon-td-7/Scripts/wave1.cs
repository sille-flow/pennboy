using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wave1 : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public GameObject enemyList;
    
    private int enemyIndex = 0;
    public int enemiesSpawned = 0;
    private float spawnRate = 1;
    private float timer = 0;
    private int enemyCount = 10;
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
                case 5:
                    enemyIndex = 2;
                    break;
                case 3:
                    enemyIndex = 1;
                    break;
            }
            //Debug.Log("enemiesSpanwed: " + enemiesSpawned + " enemyIndex: " + enemyIndex);
            GameObject newEnemy = Instantiate(enemyList.transform.GetChild(enemyIndex).gameObject, transform.position, transform.rotation);
            newEnemy.tag = "BTD7Enemy";
            enemies.Add(newEnemy);
        } 
        if (checkAllNull(enemies) && enemies.Count == enemyCount)
        {
            //Debug.Log("start next wave countdown");
            GameManager.instance.waveManager.waveOccurring = false;
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
