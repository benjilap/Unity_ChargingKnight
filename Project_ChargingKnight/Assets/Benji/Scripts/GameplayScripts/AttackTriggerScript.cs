using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackTriggerScript : MonoBehaviour {

    [HideInInspector]
    public bool isAttacking;
    [HideInInspector]
    public float knokbackPower;
    [HideInInspector]
    public float hitDamage;

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
                NavMeshAgent enemyAgt = other.GetComponent<NavMeshAgent>();
                if (enemyAgt != null)
                {
                    targetRb.isKinematic = false;
                }
                if (targetRb != null)
                {
                    LifeGlobalScript targetLife = other.GetComponent<LifeGlobalScript>();
                    targetRb.AddForce((this.transform.position - this.transform.parent.position).normalized * 100 *knokbackPower);
                    hasAttacked = true;
                    if (targetLife != null)
                    {
                        targetLife.lifeValue -= hitDamage;
                        if (enemyAgt != null)
                        {
                            StartCoroutine(EnemyRecoverMovement(targetRb));

                        }


                    }
                }
            }
        }
    }

    private IEnumerator EnemyRecoverMovement(Rigidbody enemyRigidbody)
    {
        yield return new WaitForSeconds(0.8f);
        if (enemyRigidbody != null)
        {
            enemyRigidbody.isKinematic = true;

        }
    }
}
