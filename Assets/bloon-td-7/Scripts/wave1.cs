using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wave1 : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public GameObject enemyList;
    
    private int enemyIndex = 0;
    public int enemiesSpawned;
    private float spawnRate = 1;
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
                case 10:
                    Destroy(gameObject);
                    break;
                case 5:
                    enemyIndex = 2;
                    break;
                case 3:
                    enemyIndex = 1;
                    break;
            }
            //Debug.Log("enemiesSpanwed: " + enemiesSpawned + "enemyIndex: " + enemyIndex);
            Instantiate(enemyList.transform.GetChild(enemyIndex).gameObject, transform.position, transform.rotation);
        }
    }
}
