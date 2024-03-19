using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostAI : MonoBehaviour
{

    public NavMeshAgent agent;
    public Transform heart;
    public GameObject deathSound, currentDeathSound, musicObject;

    // Start is called before the first frame update
    void Start()
    {
        heart = GameObject.FindGameObjectWithTag("Heart").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(heart.position);
        musicObject = GameObject.FindGameObjectWithTag("Music");
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(heart.position);
    }

    void OnDestroy()
    {
        currentDeathSound = Instantiate(deathSound, musicObject.transform);
        Destroy(currentDeathSound, 1f);
    }
}
