using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = .7f;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private float meleeAttackCooldown = 1.5f;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(meleeAttackCooldown <= 0)
            {
                MeleeAttack();
                meleeAttackCooldown = 2f;

            }

        }
        meleeAttackCooldown -= Time.deltaTime;
    }


    private void MeleeAttack() {
       Collider[] enemyHit =  Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayerMask);  

        foreach(Collider enemy in enemyHit)
        {
            Destroy(enemy.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
