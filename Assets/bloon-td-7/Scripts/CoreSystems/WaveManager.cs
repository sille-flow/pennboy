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
    /// <summary>
    /// EnemyInfo(float moveSpeed, int dmg, int health, int moneyWorth, Color color, float size = 5, bool isCamo = false)
    /// </summary>
    public EnemyInfo[] enemyList =
    {
        new EnemyInfo(8f,1,1,15, new Color32(0,255,0,255)),                      //0 - slime
        new EnemyInfo(25f,1,1,20, new Color32(125,209,123, 255)),        //1 - goblin
        new EnemyInfo(6f,4,5,40, new Color32(21, 92, 20, 255),8),         //2 - orcs
        new EnemyInfo(4f,10,15,60,new Color32(70, 89, 70, 255),12),        //3 - ogres
        new EnemyInfo(10f,1,1,5,new Color32(255,255,255,255)),       //4 skeleton
        new EnemyInfo(6f,10,5,15, new Color32(64, 255, 150,255)),   //5 elf
        new EnemyInfo(30f,2,1,10, new Color32(222, 182, 250,255)),  //6 fairy
        new EnemyInfo(40f,15,3,25, new Color32(117, 12, 5,255)),    //7 demon
        new EnemyInfo(8f,3,4,10, new Color32(100,100,100,255)),    //8 dwarf
        new EnemyInfo(15f,20,5,20, new Color32(40, 96, 250,255)),   //9 wizard
        new EnemyInfo(30f,40,12,50, new Color32(139, 155, 199,255)), //10 light wizard
        new EnemyInfo(30f,40,12,50, new Color32(0, 0, 46,255)), //11 dark wizard
        new EnemyInfo(15f,100,25,100, new Color32(114, 0, 252,255),8), //12 master wizard
        new EnemyInfo(200f,1000,100,1000,new Color32(255,0,0,255),20), //13 dragon
        //new EnemyInfo(30f,10,10,100,Color.cyan),        // fast assassain enemy
        //new EnemyInfo(100f,0,10000,0,Color.black),       //4 - distraction enemy
        //new EnemyInfo(3,100,1000,1000,new Color(61, 110, 173),30), //5 - boss enemy
    };
    /// <summary>
    /// WaveInfo(       all are in one string
    /// enemyIds,
    /// enemyCount,
    /// spacing,        (time in seconds between enemy spawn)
    /// time               (when cluster spawns)
    /// </summary>
    public WaveInfo[] waves =
    {
        //tutorial wave

        //wave 1 - slimes
        new WaveInfo(
                "0",
                "20",
                "0.6",
                "0"
            ),
        //wave 2 - slime + skeletons
        new WaveInfo(
                "0,4",
                "10,6",
                "0.5,0.4",
                "0,3"
            ),
        //wave 3 - many goblins
        new WaveInfo(
                "1",
                "50",
                "0.3",
                "0"
            ),
        new WaveInfo(
                "1,2,0",
                "20,1,4",
                "0.5,0.1,0.5",
                "0,5,4"
            ),
        new WaveInfo(
                "1,0,1,0,1,0",
                "10,10,10,10,10,10",
                "0.4,0.3,0.5,0.2,0.3,0.4",
                "0,2,1,3,4,5"
            ),
        new WaveInfo(
                "1,0,1,0,1,0",
                "10,10,10,10,10,10",
                "0.4,0.3,0.5,0.2,0.3",
                "0,2,1,3,4"
            ),
    };
     
    void Update()
    {
        //stop waves after final wave
        if(waveIndex >= waves.Count()) { return; }
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
        public Color32 color;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moveSpeed">How fast enemies move</param>
        /// <param name="dmg">The damage enemies deal after passing the map</param>
        /// <param name="health">The health of the enemy</param>
        /// <param name="moneyWorth">The money the enemy drops after dying</param>
        /// <param name="color">The color of the enemy</param>
        /// <param name="size">The size of the enemy</param>
        /// <param name="isCamo">Whether enemies are camoflauged or not</param>
        public EnemyInfo(float moveSpeed, int dmg, int health, int moneyWorth, Color32 color, float size = 5, bool isCamo = false)
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


