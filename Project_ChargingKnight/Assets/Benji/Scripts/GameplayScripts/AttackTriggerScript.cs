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
    [HideInInspector]
    public bool hasAttacked;
    [HideInInspector]
    public bool inBashing;


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
        }
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject objectToModify = null;
        if (other.name == "AttackZone")
        {
            objectToModify = other.transform.parent.gameObject;
        }
        else
        {
            objectToModify = other.gameObject;
        }

        if (isAttacking && !hasAttacked)
        {
            if(objectToModify.layer != this.gameObject.layer)
            {

                hasAttacked = true;
                Rigidbody targetRb = objectToModify.GetComponent<Rigidbody>();
                NavMeshAgent enemyAgt = objectToModify.GetComponent<NavMeshAgent>();
                if (enemyAgt != null)
                {
                    Debug.Log("atkknockback");

                    targetRb.isKinematic = false;
                }
                if (targetRb != null)
                {
                    LifeGlobalScript targetLife = objectToModify.GetComponent<LifeGlobalScript>();
                    targetRb.AddForce((this.transform.position+this.transform.parent.GetComponent<Rigidbody>().velocity - objectToModify.transform.position).normalized * 100 *knokbackPower);
                    if (targetLife != null)
                    {
                        targetLife.lifeValue -= hitDamage;
                        if (enemyAgt != null)
                        {
                            Debug.Log("atkhit");

                            StartCoroutine(EnemyRecoverMovement(targetRb));

                        }
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject objectToModify = null;
        if (other.name == "AttackZone")
        {
            objectToModify = other.transform.parent.gameObject;
        }
        else
        {
            objectToModify = other.gameObject;
        }

        if (this.transform.parent.GetComponent<KnightClass>() != null)
        {
            if (inBashing)
            {
                NavMeshAgent enemyAgt = objectToModify.GetComponent<NavMeshAgent>();
                Rigidbody targetRb = objectToModify.GetComponent<Rigidbody>();
                if (enemyAgt != null)
                {

                    targetRb.isKinematic = false;
                }
                if (targetRb != null)
                {
                    LifeGlobalScript targetLife = objectToModify.GetComponent<LifeGlobalScript>();
                    targetRb.AddForce((this.transform.position + this.transform.parent.GetComponent<Rigidbody>().velocity - objectToModify.transform.position).normalized * 100 * knokbackPower);
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
        yield return new WaitForSeconds(0.5f);
        if (enemyRigidbody != null)
        {
            enemyRigidbody.isKinematic = true;

        }
    }
}
