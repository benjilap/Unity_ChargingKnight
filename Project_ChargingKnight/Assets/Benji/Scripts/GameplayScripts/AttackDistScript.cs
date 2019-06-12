using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDistScript : MonoBehaviour {

    [SerializeField]
    protected float AtkCharge;
    [SerializeField]
    protected float AtkRecover;
    public float AtkDist;
    [SerializeField]
    protected float projectileSpd =1;
    [SerializeField]
    protected float projectileKnokback = 5;

    [HideInInspector]
    public bool canAttack = true;
    [HideInInspector]
    public bool launchProjectile = true;

    protected Object projectileObj;
    protected Vector3 projectileSpwPos;

    [SerializeField]
    protected bool hasToShoot;


    protected void InitVar()
    {
        projectileObj = Resources.Load("Projectiles/Projectile");
    }

    protected IEnumerator ChargeAttack()
    {
        yield return new WaitForSeconds(AtkCharge);
        hasToShoot = true;
    }

    protected IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(AtkRecover);
        canAttack = true;

    }


    protected void UpdateProjectileSpwnPos(int dirFaced)
    {
        if (dirFaced == 0)
        {
            projectileSpwPos = new Vector3(0, 0, -0.75f);
        }
        else
        if (dirFaced == 1)
        {
            projectileSpwPos = new Vector3(0.75f, 0, 0);
        }
        else
        if (dirFaced == 2)
        {
            projectileSpwPos = new Vector3(0, 0, 0.75f);
        }
        else
        if (dirFaced == 3)
        {
            projectileSpwPos = new Vector3(-0.75f, 0, 0);
        }
    }
}
