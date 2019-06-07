using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCacAttackScript : AttackScript {

    EnemyCacScript myEnemy;

    // Use this for initialization
    void Start()
    {
        InitVar();
        myEnemy = this.GetComponent<EnemyCacScript>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnemyCacAttack()
    {
        UpdateAttackZoneTrans(myEnemy.EnemyDirFaced());


        if (canAttack)
            {
                canAttack = false;
                AttackZone.isAttacking = true;

                StartCoroutine(ResetAttack());
        }
        
    }
}
