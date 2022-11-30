using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MainEnemyController : MonoBehaviour
{
    public Transform playerTransform;
    NavMeshAgent enemyNavAgent;

    // Start is called before the first frame update
    void Start()
    {
        enemyNavAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyNavAgent.SetDestination(playerTransform.position);
    }
}
