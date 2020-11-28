using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.5f;
    private float randomMinTime = 3f;
    private float randomMaxTime = 5f;
    [SerializeField] private float startWaitTime;
    [SerializeField] private float fov = 90f;
    [SerializeField] private float viewDistance = 10f;

    [SerializeField] private Transform player;
    [SerializeField] private Transform[] patrolPos;
    [SerializeField] private Transform pffieldOfView;

    [SerializeField] private bool seePlayer;
    [SerializeField] private bool goingLeft;
    [SerializeField] private bool goingRight;

    private FieldOfView fieldOfView;
    [SerializeField] private int nextPatrolPos = 0;
    private Rigidbody rb;

    private Vector3 fovLeft = new Vector3(-10f, 0f, 0f);
    private Vector3 fovRight = new Vector3(10f, 0f, 0f);



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

    private void Start()
    {
        fieldOfView = Instantiate(pffieldOfView, null).GetComponent<FieldOfView>();
        fieldOfView.SetFOV(fov);
        fieldOfView.SetViewDistance(viewDistance);
    }

    private void Update()
    {
        goingLeft = !goingRight;
        if(seePlayer)
            enemyState = EnemyState.Chase;

    }


    private void FixedUpdate()
    {
        seePlayer = false;
        FindTargetPlayer();

        fieldOfView.SetOrigin(transform.position);

        if (rb.velocity.x > 0)
        {
            goingRight = true;
            fieldOfView.SetDirection(fovRight);
        } else if(rb.velocity.x < 0)
        {
            fieldOfView.SetDirection(fovLeft);
            goingRight = false;
        }

        switch (enemyState)
        {
            case EnemyState.Patrol:
                Patrol();
                break;
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Chase:
                ChasePlayer();
                break;
        }

    }

    private void Patrol()
    {
        if(transform.position.x - patrolPos[nextPatrolPos].transform.position.x < 0)
        {
            rb.velocity = new Vector3(moveSpeed, rb.velocity.y, 0f);

        } else if(transform.position.x - patrolPos[nextPatrolPos].transform.position.x > 0)
        {
            rb.velocity = new Vector3(-moveSpeed, rb.velocity.y, 0f);
        }


        if (Vector3.Distance(transform.position, patrolPos[nextPatrolPos].transform.position) < .2f)
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
            if (startWaitTime <= 0)
            {
                nextPatrolPos += 1;
                if (nextPatrolPos > 1)
                    nextPatrolPos = 0;
                startWaitTime = Random.Range(randomMinTime, randomMaxTime);

            }
            else enemyState = EnemyState.Idle;
        }
    }


    private void FindTargetPlayer()
    {
        if (Vector3.Distance(GetPosition(), player.position) < viewDistance)
        {
            Vector3 dirToPlayer = (player.position - GetPosition()).normalized;

            if(goingLeft)
            {
                if (Vector3.Angle(fovLeft, dirToPlayer) < fov/2f)
                {
                    Ray ray = new Ray(GetPosition(), dirToPlayer * viewDistance);
                    RaycastHit hit;
                    Physics.Raycast(ray, out hit);
                    if (hit.collider != null && hit.transform.tag == "Player")
                    {
                        seePlayer = true;

                    }

                }

            }
            else if (goingRight)
            {
                if (Vector3.Angle(fovRight, dirToPlayer) < fov/2f)
                {
                    Ray ray = new Ray(GetPosition(), dirToPlayer * viewDistance);
                    RaycastHit hit;

                    Physics.Raycast(ray, out hit);
                    if (hit.collider != null && hit.transform.tag == "Player")
                    {
                        seePlayer = true;

                    }
                }
            }
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

    private Vector3 GetPosition()
    {
        return transform.position;
    }

    private void ChasePlayer()
    {
        if(GetPosition().x - player.transform.position.x < 0)
        {
            rb.velocity = new Vector3(moveSpeed, rb.velocity.y, 0f);

        }
        else if (GetPosition().x - player.transform.position.x > 0)
        {
            rb.velocity = new Vector3(-moveSpeed, rb.velocity.y, 0f);

        }



        if (Vector3.Distance(new Vector3(GetPosition().x, GetPosition().y * 2f, 0f), player.position) > viewDistance)
        {
            startWaitTime = Random.Range(randomMinTime, randomMaxTime);
            enemyState = EnemyState.Idle;
        }
    }

}
