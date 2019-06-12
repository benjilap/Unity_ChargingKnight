using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

    protected Rigidbody projectileRb;

    [HideInInspector]
    public string instantiatorName;
    [HideInInspector]
    public float knokbackPower;

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
        if (other.gameObject.layer != this.gameObject.layer)
        { 
            Rigidbody targetRb = other.GetComponent<Rigidbody>();
            if (targetRb != null)
            {
                targetRb.AddForce(projectileRb.velocity.normalized * 100 * knokbackPower);
            }
            
            Destroy(this.gameObject, 0.05f);
        }
    }
}
