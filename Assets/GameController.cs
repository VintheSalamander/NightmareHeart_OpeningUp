using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEditor.PackageManager.Requests;

public class GameController : MonoBehaviour
{
    [Serializable]
    public class WaveStats
    {
        public int zombieMaxAmount, totalZombiesSpawned, waveNumber, zombieWaveIncrease;
        public int ghostMaxAmount, totalGhostsSpawned, ghostWaveIncrease;

        public int bossMaxAmount, totalBossSpawned, bossWaveIncrease;
        public bool done, spawnBoss, reset, bossTimer;
    }
    public GameObject playerPrefab;
    public List<GameObject> allEnemies;
    public int i;
    public WaveStats waveStats;
    public GameObject enemyParent;
    public Transform playerSpawn;
    public Transform enemySpawn;

    public GameObject zombieAI, ghostAI, bossAI, enemyToAdd;
    public static GameController current;
    //public event Action<> onSpawnNewPlayer;
    // Start is called before the first frame update
    void Start()
    {
        SpawnNewPlayer();
        current = this;
        Invoke("WaveSpawn", 5f);
    }

    public void SpawnNewPlayer()
    {
        Instantiate(playerPrefab, playerSpawn.transform.position, quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        EndWave();
    }

    void EndWave()
    {
        if(waveStats.done && allEnemies.Count == 0 && !waveStats.reset)
        {
            waveStats.reset = true;
            Invoke("NextWave", 10f);
        }
    }

    void NextWave()
    {
        waveStats.done = false;
        waveStats.waveNumber += 1;
        waveStats.bossMaxAmount += waveStats.bossWaveIncrease;
        waveStats.zombieMaxAmount += waveStats.zombieWaveIncrease;
        waveStats.ghostMaxAmount += waveStats.ghostWaveIncrease;
        waveStats.totalBossSpawned = 0;
        waveStats.totalZombiesSpawned = 0;
        waveStats.totalGhostsSpawned = 0;
        waveStats.reset = false;
        Invoke("WaveSpawn", 3f);
    }

    void BossTimerOn()
    {
        waveStats.bossTimer = false;
    }

    void WaveSpawn()
    {

        if( waveStats.totalZombiesSpawned == waveStats.zombieMaxAmount && waveStats.totalGhostsSpawned == waveStats.ghostMaxAmount)
        {
            if(waveStats.totalBossSpawned < waveStats.bossMaxAmount && !waveStats.bossTimer)
            {
                waveStats.bossTimer = true;
                waveStats.spawnBoss = true;
                Invoke("BossTimerOn", 5);
            }
            else if(waveStats.totalBossSpawned == waveStats.bossMaxAmount)
            {
                waveStats.done = true;
            }
            
        }

        if(waveStats.spawnBoss)
        {
            waveStats.spawnBoss = false;
            enemyToAdd = Instantiate(bossAI, enemySpawn.position, quaternion.identity, enemyParent.transform);
            allEnemies.Add(enemyToAdd);
            waveStats.totalBossSpawned += 1;
           // waveStats.bossCurrentAmount += 1;
            Debug.Log("test");
        }

        i = UnityEngine.Random.Range(0,2);
        if(waveStats.totalZombiesSpawned == waveStats.zombieMaxAmount)
        {
            i = 1;
        }

        if(waveStats.totalGhostsSpawned == waveStats.ghostMaxAmount)
        {
            i = 0;
        }

        if(i == 0)
        {
            if(waveStats.totalZombiesSpawned < waveStats.zombieMaxAmount)
            {
                enemyToAdd = Instantiate(zombieAI, enemySpawn.position, quaternion.identity, enemyParent.transform);
                allEnemies.Add(enemyToAdd);
                waveStats.totalZombiesSpawned += 1;
                //waveStats.zombieCurrentAmount += 1;
            }
        }
        else{

            if(waveStats.totalGhostsSpawned < waveStats.ghostMaxAmount)
            {
                enemyToAdd = Instantiate(ghostAI, enemySpawn.position, quaternion.identity, enemyParent.transform);
                allEnemies.Add(enemyToAdd);
                waveStats.totalGhostsSpawned += 1;
                //waveStats.ghostCurrentAmount += 1;
            }
        
        }

        if(!waveStats.done)
        {
            i = UnityEngine.Random.Range(1,6);
            Debug.Log(i);
            Invoke("WaveSpawn", i);

        }
    }
}
