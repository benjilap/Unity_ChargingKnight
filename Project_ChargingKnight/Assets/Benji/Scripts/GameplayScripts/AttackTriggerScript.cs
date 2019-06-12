﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTriggerScript : MonoBehaviour {

    [HideInInspector]
    public bool isAttacking;

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

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (isAttacking)
    //    {

    //    }
    //}
}