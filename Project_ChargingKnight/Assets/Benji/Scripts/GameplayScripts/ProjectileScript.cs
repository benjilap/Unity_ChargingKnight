using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

    protected Rigidbody projectileRb;

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
        Destroy(this.gameObject, 0.3f);
    }
}
