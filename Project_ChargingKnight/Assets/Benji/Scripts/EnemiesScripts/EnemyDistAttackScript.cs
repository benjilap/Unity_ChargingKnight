using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDistAttackScript : AttackDistScript {

    EnemyDistScript myEnemy;

    void Start()
    {
        //InitVar();
        myEnemy = this.GetComponent<EnemyDistScript>();

    }

    public void EnemyDistAttack()
    {


        if (canAttack)
        {
            canAttack = false;

            StartCoroutine(ResetAttack());
        }

    }
}
