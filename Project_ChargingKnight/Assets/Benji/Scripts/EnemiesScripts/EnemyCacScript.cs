using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCacScript : EnemyScript {

    EnemyCacAttackScript enemyAttack;

	// Use this for initialization
	void Start () {
        InitLayerMask();
        InitVar();
        enemyAttack = this.GetComponent<EnemyCacAttackScript>();
    }
	
	// Update is called once per frame
	void Update () {
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
        if(enemyNavAgent.destination != TargetSelection().transform.position)
        {
            enemyNavAgent.SetDestination(TargetSelection().transform.position);
        }

        if(Vector3.Distance(this.transform.position, enemyNavAgent.destination) <= 2)
        {
            enemyNavAgent.SetDestination(this.transform.position);

            enemyAttack.EnemyCacAttack();
        }
    }
}
