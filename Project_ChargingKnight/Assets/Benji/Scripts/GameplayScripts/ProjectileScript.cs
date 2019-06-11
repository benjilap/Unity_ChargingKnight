using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

    protected Rigidbody projectileRb;

    [HideInInspector]
    public string instantiatorName;

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
        this.transform.rotation = Quaternion.LookRotation(projectileRb.velocity.normalized);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != this.gameObject.layer)
        {
            Destroy(this.gameObject, 0.1f);

        }
    }
}
