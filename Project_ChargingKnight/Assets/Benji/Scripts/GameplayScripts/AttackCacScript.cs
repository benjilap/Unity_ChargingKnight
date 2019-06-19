using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCacScript : MonoBehaviour {

    public float AtkKnokback = 5;
    public float AtkCharge;
    public float AtkDuration;
    public float AtkRecover;
    public float AtkDamage = 10;

    [HideInInspector]
    public bool canAttack = true;

    protected AttackTriggerScript AttackZone;

    protected void InitVar()
    {
        AttackZone = this.transform.Find("AttackZone").GetComponent<AttackTriggerScript>();
        AttackZone.knokbackPower = AtkKnokback;
        AttackZone.hitDamage = AtkDamage;
    }

    protected IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(AtkDuration);
        AttackZone.isAttacking = false;
        yield return new WaitForSeconds(AtkRecover);
        canAttack = true;
        AttackZone.hasAttacked = false;

    }

    protected IEnumerator AttackAction()
    {
        yield return new WaitForSeconds(AtkCharge);
        AttackZone.isAttacking = true;

        StartCoroutine(ResetAttack());
    }

    protected void UpdateAttackZoneTrans(int dirFaced)
    {
        if (dirFaced == 0)
        {
            AttackZone.transform.localPosition = new Vector3(0, 0, -0.9f);
            AttackZone.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        if (dirFaced == 1)
        {
            AttackZone.transform.localPosition = new Vector3(0.9f, 0, 0);
            AttackZone.transform.eulerAngles = new Vector3(0, 90, 0);
        }
        else
        if (dirFaced == 2)
        {
            AttackZone.transform.localPosition = new Vector3(0, 0, 0.9f);
            AttackZone.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        if (dirFaced == 3)
        {
            AttackZone.transform.localPosition = new Vector3(-0.9f, 0, 0);
            AttackZone.transform.eulerAngles = new Vector3(0, 90, 0);
        }
    }
}
