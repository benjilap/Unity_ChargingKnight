using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTriggerScript : MonoBehaviour {

    [HideInInspector]
    public bool isAttacking;
    [HideInInspector]
    public float knokbackPower;

    bool hasAttacked;

    private void Update()
    {
        TriggerActive();
    }

    private void TriggerActive()
    {
        if (isAttacking)
        {
            this.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            this.GetComponent<MeshRenderer>().enabled = false;
            hasAttacked = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isAttacking && !hasAttacked)
        {
            if( other.gameObject.layer != this.gameObject.layer)
            {
                Rigidbody targetRb = other.GetComponent<Rigidbody>();
                if (targetRb != null)
                {

                    targetRb.AddForce((this.transform.position - this.transform.parent.position).normalized * 100 *knokbackPower);
                    hasAttacked = true;
                }
            }
        }
    }
}
