using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementAI : MonoBehaviour
{
    public Transform WayPoint;
    // Start is called before the first frame update
    void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(WayPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
