using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {

    PlayerClass playerClss;
    AttackTriggerScript playerAtkTrigger;
    Animator myAtor;



	void Start () {
        playerClss = this.transform.parent.GetComponent<PlayerClass>();
        playerAtkTrigger = this.transform.parent.Find("AttackZone").GetComponent<AttackTriggerScript>();

        myAtor = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateDirFaced();
        UpdateAtkAnim();

    }

    void UpdateDirFaced()
    {
        myAtor.SetInteger("PlayerDirFaced", playerClss.PlayerDirFaced());
    }

    void UpdateAtkAnim()
    {

        SetAtorBool("AtkEnable", playerAtkTrigger.inAttack);
    

        SetAtorBool("SlashEnable", playerAtkTrigger.inRazor);
        SetAtorBool("BashEnable", playerAtkTrigger.inBashing);
        SetAtorBool("DodgeEnable", !playerClss.hittable);

    }

    void SetAtorBool(string boolVar, bool actionVar)
    {

            if (myAtor.GetBool(boolVar) != actionVar)
            {

                myAtor.SetBool(boolVar, actionVar);
            }


    }

    
}
