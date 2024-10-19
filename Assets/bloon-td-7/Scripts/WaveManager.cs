using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private float waveCooldown = 1f;
    public float globalTimer = 0;
    public bool waveOccurring = false;
    public GameObject waveList;
    public int waveIndex = 0;
    public List<Spawner> spawners;
    public int spawnersCreated = 0;
    [SerializeField] private TextMeshProUGUI roundText;

    public float clusterTimer = 0;

    // Start is called before the first frame update
    private void Start()
    {
        spawners = new List<Spawner>();
    }
    // Update is called once per frame
    public EnemyInfo[] enemyList =
    {
        new EnemyInfo(15f,1,2,15, Color.red),
        new EnemyInfo(40f,1000,1,30, Color.blue),
        new EnemyInfo(10f, 20, 5, 100, Color.yellow)
    };

    public WaveInfo[] waves =
    {
        new WaveInfo(
                "0,1",
                "3,5",
                "0.1,0.6",
                "0,5"
            ),
        new WaveInfo(
                "1,2,0",
                "20,1,4",
                "0.5,0.1,0.5",
                "0,5,4"
            )
    };
     
    void Update()
    {
        roundText.text = "Round:\n" + waveIndex;
        if (globalTimer < waveCooldown && !waveOccurring)
        {
            globalTimer += Time.deltaTime;
        }
        else
        {
            if (!waveOccurring)
            {   
                //Debug.Log(waveIndex);
                WaveInfo currWave = waves[waveIndex];
                for (int i = 0; i < waves[waveIndex].enemyIdList.Count(); i++)
                {
                    new Spawner(currWave.enemyIdList[i], currWave.enemyCount[i], currWave.spacing[i], currWave.time[i]);
                }
                //Instantiate(waveList.transform.GetChild(waveIndex).gameObject, transform.position, transform.rotation);
                waveOccurring = true;
                waveIndex++;
                globalTimer = 0;
            } else
            {
                for (int i = 0; i < spawners.Count; i++)
                {
                    spawners[i].Update();
                }
                //Debug.Log(waves[waveIndex-1].enemyIdList.Count());
                //waveIndex has been updated
                if (spawners.Count == 0 && spawnersCreated == waves[waveIndex-1].enemyIdList.Count())
                {
                    waveOccurring = false;
                    spawnersCreated = 0;
                }
            }
        }

    }

    public class EnemyInfo
    {
        public float moveSpeed;
        public int dmg;
        public int health;
        public int moneyWorth;
        public bool isCamo;
        public float size;
        public Color color;

        public EnemyInfo(float moveSpeed, int dmg, int health, int moneyWorth, Color color, float size = 5, bool isCamo = false)
        {
            this.moveSpeed = moveSpeed;
            this.dmg = dmg;
            this.health = health;
            this.moneyWorth = moneyWorth;
            this.isCamo = isCamo;
            this.size = size;
            this.color = color;
        }
    }

    public class WaveInfo
    {
        public int[] enemyIdList;
        public int[] enemyCount;
        public float[] spacing;
        public float[] time;


        /// <summary>
        /// All arrays are connected such that the index of each array represents a cluster of enemies.
        /// </summary>
        /// <param name="enemyIdList">
        /// List of enemies that will appear in order. Values correspond to index in the EnemyList array
        /// </param>
        /// <param name="enemyCount">
        /// List of how many of each enemy will spawn corresponding to the index
        /// </param>
        /// <param name="spacing">
        /// The space between each enemies in the cluster.
        /// </param>
        /// <param name="time">
        /// The time at which the cluster spawns.
        /// </param>
        public WaveInfo(string enemyIdList, string enemyCount, string spacing, string time)
        {
            this.enemyIdList = enemyIdList?.Split(',')?.Select(int.Parse)?.ToArray();
            this.enemyCount = enemyCount?.Split(',')?.Select(int.Parse)?.ToArray();
            this.spacing = spacing?.Split(',')?.Select(float.Parse)?.ToArray();
            this.time = time?.Split(',')?.Select(float.Parse)?.ToArray();
        }

    }
    public class Spawner
    {
        public List<GameObject> enemies = new List<GameObject>();
        public float timer;
        int enemyId;
        int enemySpawned = 0;
        int enemyCount;
        float spacing;
        float start_time;
       
        //one for loop on all spawners and call this
        public void Update()
        {
            if (timer < start_time + spacing && enemySpawned < enemyCount)
            {
                timer += Time.deltaTime;
            } else if (timer >= start_time + spacing)
            {

                EnemyInfo enemyInfo = GameManager.instance.waveManager.enemyList[enemyId];
                GameObject newEnemy = Instantiate(GameManager.instance.enemy,
                    GameManager.instance.waveManager.gameObject.transform.position,
                    GameManager.instance.waveManager.gameObject.transform.rotation);
                newEnemy.GetComponent<Enemy>().Initialize(enemyInfo.moveSpeed, enemyInfo.dmg, enemyInfo.health, enemyId, enemyInfo.moneyWorth, enemyInfo.isCamo, enemyInfo.size);
                newEnemy.GetComponent<Renderer>().material.color = enemyInfo.color;
                enemies.Add(newEnemy);
                enemySpawned++;
                timer = start_time;
            }
            //Debug.Log(checkAllNull(enemies));
            if (checkAllNull(enemies) && enemies.Count == enemyCount)
            {
                Debug.Log("killed cluster");
                GameManager.instance.waveManager.spawners.Remove(this);
            }

        }
        public Spawner (int enemyId, int enemyCount, float spacing, float time) 
        {
            this.enemyId = enemyId;
            this.enemyCount = enemyCount;
            this.spacing = spacing;
            this.start_time = time;
            GameManager.instance.waveManager.spawners.Add(this);
            GameManager.instance.waveManager.spawnersCreated++;
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
}


