using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MainEnemyController : MonoBehaviour
{

    [Header("Movement")]
    public float speed;
    public float chaseSpeed;

    [Header("Positions")]
    Vector3 activeFollowPos;
    bool isPatrol = false;

    [Header("Refs")]
    public Transform playerTransform;
    public EnemyFOV enemyFOV;
    NavMeshAgent enemyNavAgent;
    Animator animator;

    public Vector3 Vel;
    public float distanceToTar;
    Vector3 previous;
    
    [Header("HashStuffs")]
    int idleHash = Animator.StringToHash("Root_Idle2");



    public MovementState state;
    public enum MovementState
    {
        isIdle,
        isWalking,
        isChasing,
        isStunned,
        isInIntro,
        isSearching,
        isGotPlayer
    }

    void Start()
    {
        enemyFOV.targetLastKnowPos = transform.position;
        enemyNavAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyNavAgent.SetDestination(activeFollowPos);

        StateHandler();
        AnimHandler();

        Vel = (transform.position - previous) / Time.deltaTime;
        distanceToTar = (Vector3.Distance(transform.position, activeFollowPos));
        previous = transform.position;
    }

    private void StateHandler()
    {
        if (enemyFOV.canSeePlayer)
        {
            state = MovementState.isChasing;
            enemyNavAgent.speed = chaseSpeed;
            activeFollowPos = playerTransform.position;
        }
        else if (!enemyFOV.canSeePlayer)
        {          
            enemyNavAgent.speed = speed;
            activeFollowPos = enemyFOV.targetLastKnowPos;
            if( Vel.x == 0 && Vel.y == 0 )state = MovementState.isIdle;
            else state = MovementState.isSearching;
            
        }
    }

    private void AnimHandler()
    {
        animator.SetBool("Idle", state == MovementState.isIdle);
        if(state == MovementState.isSearching) animator.SetInteger("Movement", 1);
        else if(state == MovementState.isChasing) animator.SetInteger("Movement", 2);
        else animator.SetInteger("Movement", 0);


        //if ((Vel.x != 0.1 || Vel.z != 0.1)) animator.SetInteger("Movement", 2);
    }

    private IEnumerator LostSight()
    {
        yield return new WaitForSeconds(2f);
    }
}
