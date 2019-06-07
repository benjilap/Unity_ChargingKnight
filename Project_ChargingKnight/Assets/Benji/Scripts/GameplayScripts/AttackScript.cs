using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour {

    [SerializeField]
    protected float AtkDuration;
    [SerializeField]
    protected float AtkRecover;
    
    [HideInInspector]
    public bool canAttack = true;

    protected AttackTriggerScript AttackZone;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected void InitVar()
    {
        AttackZone = this.transform.Find("AttackZone").GetComponent<AttackTriggerScript>();

    }



    protected IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(AtkDuration);
        AttackZone.isAttacking = false;
        yield return new WaitForSeconds(AtkRecover);
        canAttack = true;

    }

    protected void UpdateAttackZoneTrans(int dirFaced)
    {
        if (dirFaced == 0)
        {
            AttackZone.transform.localPosition = new Vector3(0, 0, -0.75f);
            AttackZone.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        if (dirFaced == 1)
        {
            AttackZone.transform.localPosition = new Vector3(0.75f, 0, 0);
            AttackZone.transform.eulerAngles = new Vector3(0, 90, 0);
        }
        else
        if (dirFaced == 2)
        {
            AttackZone.transform.localPosition = new Vector3(0, 0, 0.75f);
            AttackZone.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        if (dirFaced == 3)
        {
            AttackZone.transform.localPosition = new Vector3(-0.75f, 0, 0);
            AttackZone.transform.eulerAngles = new Vector3(0, 90, 0);
        }
    }
}
