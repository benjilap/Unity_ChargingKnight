using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDistAttackScript : AttackDistScript {

    EnemyDistScript myEnemy;
    GameObject newProjectile;

    void Start()
    {
        InitVar();
        myEnemy = this.GetComponent<EnemyDistScript>();

    }

    public void EnemyDistAttack()
    {
        UpdateProjectileSpwnPos(myEnemy.EnemyDirFaced());
        if (newProjectile == null)
        {
            if (canAttack)
            {
                canAttack = false;
                newProjectile = Instantiate(projectileObj, this.transform.position + projectileSpwPos, Quaternion.identity) as GameObject;
                newProjectile.GetComponent<ProjectileScript>().instantiatorName = this.gameObject.name;
                newProjectile.layer = this.gameObject.layer;
                StartCoroutine(ChargeAttack());
            }
        }
        else
        {
            newProjectile.transform.position = this.transform.position + projectileSpwPos;
            if (hasToShoot == true)
            {
                hasToShoot = false;
                newProjectile.GetComponent<Rigidbody>().velocity = (myEnemy.targetPos - this.transform.position) * projectileSpd;
                newProjectile = null;

                StartCoroutine(ResetAttack());

            }

        }

    }
}
