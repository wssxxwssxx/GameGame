using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float asd;
    [SerializeField] private Transform[] patrolPos;

    private float distanceCheck = .2f;
    private Rigidbody rb;
    private float randomMinTime = 3f;
    private float randomMaxTime = 5f;
    [SerializeField] private float startWaitTime;
    [SerializeField] private int movementDirection = 0;
    
    

    private enum EnemyState
    {
        Idle,
        Patrol,
        Chase,
        Attack,
    }

   [SerializeField] private EnemyState enemyState;

    private void Awake()
    {
        startWaitTime = Random.Range(randomMinTime, randomMaxTime);
        rb = GetComponent<Rigidbody>();
        enemyState = EnemyState.Patrol;

    }



    private void Update()
    {
        switch(enemyState)
        {
            case EnemyState.Patrol:
                Patrol();
                break;
            case EnemyState.Idle:
                Idle();
                break;
        }


    }

    private void Patrol()
    {
        transform.position = Vector3.MoveTowards(transform.position,
           new Vector3(patrolPos[movementDirection].transform.position.x, transform.position.y, transform.position.z),
           moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, patrolPos[movementDirection].transform.position) < distanceCheck)
        {

            if (startWaitTime <= 0)
            {
                movementDirection += 1;
                if (movementDirection > 1)
                    movementDirection = 0;

                startWaitTime = Random.Range(randomMinTime, randomMaxTime);


            }
            else enemyState = EnemyState.Idle;
        }
    }


    private void Idle()
    {
        if (startWaitTime <= 0)
        {
            enemyState = EnemyState.Patrol;
        }
        else startWaitTime -= Time.deltaTime;
    }


    

}
