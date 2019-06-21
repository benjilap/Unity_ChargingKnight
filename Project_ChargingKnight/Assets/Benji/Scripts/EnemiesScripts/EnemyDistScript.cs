using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDistScript : EnemyScript {

    EnemyDistAttackScript enemyAttack;

    [HideInInspector]
    public Vector3 targetPos;

    // Use this for initialization
    void Start()
    {
        InitLayerMask();
        InitVar();
        enemyAttack = this.GetComponent<EnemyDistAttackScript>();
    }

    // Update is called once per frame
    void Update()
    {
        FovRadar();

        if (targetsList.Count != 0)
        {
            MoveToAttack();
        }
        else
        {
            EnemyPatrolling();

        }
    }

    void MoveToAttack()
    {

        targetPos = TargetSelection().transform.position + TargetSelection().GetComponent<Rigidbody>().velocity;
        if (Vector3.Distance(this.transform.position, TargetSelection().transform.position) <= AtkDist)
        {
            Debug.Log("canAtk");
            enemyNavAgent.SetDestination(this.transform.position);
            enemyAttack.EnemyDistAttack();

        }
        else if (Vector3.Distance(this.transform.position, TargetSelection().transform.position) > AtkDist)
        {
            if (enemyNavAgent.destination != TargetSelection().transform.position)
            {
                enemyNavAgent.SetDestination(TargetSelection().transform.position);
            }
        }
    }
}
