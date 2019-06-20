using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

    protected Rigidbody projectileRb;

    [HideInInspector]
    public string instantiatorName;
    [HideInInspector]
    public float knokbackPower;
    [HideInInspector]
    public float projectileDmg;

    bool hasHit;

    private void Start()
    {
        InitVar();
    }

    void Update()
    {
        LookAtDirection();
    }

    void InitVar()
    {
        projectileRb = this.GetComponent<Rigidbody>();
    }

    void LookAtDirection()
    {
        if (projectileRb.velocity.magnitude != 0)
        {
            this.transform.rotation = Quaternion.LookRotation(projectileRb.velocity.normalized);
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
        if (objectToModify.layer != this.gameObject.layer)
        { 
            Rigidbody targetRb = objectToModify.GetComponent<Rigidbody>();
            LifeGlobalScript targetLife = objectToModify.GetComponent<LifeGlobalScript>();

            if (!hasHit)
            {
                if (targetRb != null)
                {
                    hasHit = true;
                    targetRb.AddForce(projectileRb.velocity.normalized * 100 * knokbackPower);
                    if (targetLife != null)
                    {
                        targetLife.lifeValue -= projectileDmg;
                    }
                }
            }

            Destroy(this.gameObject, 0.05f);
        }
    }
}
