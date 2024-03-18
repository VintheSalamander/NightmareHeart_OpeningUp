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
        public int zombieCurrentAmount, zombieMaxAmount, totalZombiesSpawned, waveNumber, zombieWaveIncrease;
        public int ghostCurrentAmount, ghostMaxAmount, totalGhostsSpawned, ghostWaveIncrease;

        public int bossCurrentAmount, bossMaxAmount, totalBossSpawned, bossWaveIncrease;
        public bool done, spawnBoss, reset;
    }
    public GameObject playerPrefab;
    public List<GameObject> allEnemies;
    public int i;
    public WaveStats waveStats;
    public Transform playerSpawn;
    public Transform enemySpawn;

    public GameObject zombieAI, ghostAI, bossAI, enemyToAdd;
    public static GameController current;
    //public event Action<> onSpawnNewPlayer;
    // Start is called before the first frame update
    void Start()
    {
        current = this;
        Invoke("WaveSpawn", 1f);
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

    void WaveSpawn()
    {

        if( waveStats.totalZombiesSpawned == waveStats.zombieMaxAmount && waveStats.totalGhostsSpawned == waveStats.ghostMaxAmount)
        {
            if(waveStats.bossCurrentAmount < waveStats.bossMaxAmount)
            {
                waveStats.spawnBoss = true;
            }
            else if(waveStats.bossCurrentAmount == waveStats.bossMaxAmount)
            {
                waveStats.done = true;
            }
            
        }

        if(waveStats.spawnBoss)
        {
            waveStats.spawnBoss = false;
            enemyToAdd = Instantiate(bossAI, enemySpawn.position, quaternion.identity);
            allEnemies.Add(enemyToAdd);
            waveStats.totalBossSpawned += 1;
           // waveStats.bossCurrentAmount += 1;
            Debug.Log("test");
        }

        if(waveStats.totalZombiesSpawned < waveStats.zombieMaxAmount)
        {
            enemyToAdd = Instantiate(zombieAI, enemySpawn.position, quaternion.identity);
            allEnemies.Add(enemyToAdd);
            waveStats.totalZombiesSpawned += 1;
            //waveStats.zombieCurrentAmount += 1;
        }

        if(waveStats.totalGhostsSpawned < waveStats.ghostMaxAmount)
        {
            enemyToAdd = Instantiate(ghostAI, enemySpawn.position, quaternion.identity);
            allEnemies.Add(enemyToAdd);
            waveStats.totalGhostsSpawned += 1;
            //waveStats.ghostCurrentAmount += 1;
        }


        if(!waveStats.done)
        {
            i = UnityEngine.Random.Range(1,4);
            Invoke("WaveSpawn", i);

        }
    }
}
