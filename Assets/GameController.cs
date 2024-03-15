using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform playerSpawn;
    public static GameController current;
    //public event Action<> onSpawnNewPlayer;
    // Start is called before the first frame update
    void Start()
    {
        current = this;
    }

    public void SpawnNewPlayer()
    {
        Instantiate(playerPrefab, playerSpawn.transform.position, quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
