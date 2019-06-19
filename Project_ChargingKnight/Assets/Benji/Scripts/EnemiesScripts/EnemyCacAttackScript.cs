using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCacAttackScript : AttackCacScript {

    EnemyCacScript myEnemy;

    void Start()
    {
        InitVar();
        myEnemy = this.GetComponent<EnemyCacScript>();

    }

    public void EnemyCacAttack()
    {
        UpdateAttackZoneTrans(myEnemy.EnemyDirFaced());


        if (canAttack)
        {
                canAttack = false;

                StartCoroutine(AttackAction());
        }
        
    }
}
