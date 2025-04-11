using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SnowBallAI : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform player;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (player)
        {
            //agent.destination = player.position;
            agent.SetDestination(player.transform.position);
        }        
    }
}
