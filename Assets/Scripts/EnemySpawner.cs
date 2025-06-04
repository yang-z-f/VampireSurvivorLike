using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    public Transform point1;
    public Transform point2;
    private Vector3 spawnPoint;
    private Transform target;
    private float despawnDistance;
    public List<GameObject> spawnEnemyList = new List<GameObject>();
    private int checkPerFrame =10;
    private int enemyToCheck;
    private int checkTarget;
    [SerializeField] private int currentWave;
    private float waveTimer;
    private float spawnTimer;
    private float random1;
    public List<WaveInfo> waves = new List<WaveInfo>();
    void Start()
    {
        target = PlayerController.instance.transform;
        despawnDistance = Vector3.Distance(transform.position, point2.position)+3;
        waveTimer = waves[0].waveLifeTime;
        spawnTimer = waves[0].timeBetweenSpawn;
    }
    void Update()
    {
        transform.position = target.position;

        EnemySpawn();
    }
    private void EnemySpawn()
    {
        if (PlayerController.instance.gameObject.activeSelf)
        {
            if (currentWave < waves.Count)
            {
                waveTimer -= Time.deltaTime;
                spawnTimer -= Time.deltaTime;
                if (spawnTimer <= 0)
                {
                    spawnTimer = waves[currentWave].timeBetweenSpawn;
                    GameObject Enemy = Instantiate(waves[currentWave].enemyToSpawn, SelectEnemySpawnerPoint(), transform.rotation);
                    spawnEnemyList.Add(Enemy);
                }
                if (waveTimer <= 0)
                {
                    currentWave++;
                    if (currentWave < waves.Count)
                    {
                        waveTimer = waves[currentWave].waveLifeTime;
                        spawnTimer = waves[currentWave].timeBetweenSpawn;
                    }
                    else
                    {
                        waveTimer = 0;
                        spawnTimer = 0;
                    }
                   
                }         
            }
        }
       
    }
    private Vector3 SelectEnemySpawnerPoint()
    {
        random1 = Random.Range(0f, 1f);
        if (random1 < .25f)
        {
            spawnPoint.y = point1.position.y;
            spawnPoint.x = Random.Range(point1.position.x, point2.position.x);
        }
        else if (random1 < .5f)
        {
            spawnPoint.y = point2.position.y;
            spawnPoint.x = Random.Range(point1.position.x, point2.position.x);
        }
        else if (random1 < .75f)
        {
            spawnPoint.x = point1.position.x;
            spawnPoint.y = Random.Range(point1.position.y, point2.position.y);
        }
        else
        {
            spawnPoint.x = point2.position.x;
            spawnPoint.y = Random.Range(point1.position.y, point2.position.y);
        }        
                
         
        return spawnPoint;
    }
    private void Despawn()
    {
        checkTarget = enemyToCheck + checkPerFrame;
        while (enemyToCheck < checkTarget)
        {
            if(enemyToCheck < spawnEnemyList.Count)
            {
                if (spawnEnemyList[enemyToCheck]!= null)
                {
                   if( Vector3.Distance(transform.position, spawnEnemyList[enemyToCheck].transform.position) > despawnDistance)
                    {
                        Destroy(spawnEnemyList[enemyToCheck]);
                        spawnEnemyList.RemoveAt(enemyToCheck);
                        checkTarget --;
                    }
                    else
                    {
                        enemyToCheck++;
                    }

                }
                else
                {
                    spawnEnemyList.RemoveAt(enemyToCheck);
                    checkTarget--; 
                }
            }
            else
            {
                enemyToCheck = 0;
                checkTarget = 0;
            }
        }
    }
}
[System.Serializable]
public class WaveInfo
{
    public GameObject enemyToSpawn;
    public float waveLifeTime;
    public float timeBetweenSpawn;
}