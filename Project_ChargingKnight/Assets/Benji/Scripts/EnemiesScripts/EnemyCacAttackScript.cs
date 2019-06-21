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
        UpdateAttackZoneTrans(myEnemy.EnemyDirFaced(),0.75f);


        if (canAttack)
        {
                canAttack = false;

                StartCoroutine(AttackAction());
        }
        
    }
}
