using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform heart;
    // Start is called before the first frame update
    void Start()
    {
        heart = GameObject.FindGameObjectWithTag("Heart").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(heart.position);
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(heart.position);
    }
}
