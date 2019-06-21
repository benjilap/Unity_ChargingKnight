using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCacAttackScript : AttackCacScript {

    PlayerClass myPlayer;
    PlayerController myPlayerController;

	// Use this for initialization
	void Start () {
        InitVar();
        myPlayer = this.GetComponent<PlayerClass>();
        myPlayerController = this.GetComponent<PlayerController>();

    }

    public void PlayerAttack()
    {
        UpdateAttackZoneTrans(myPlayer.PlayerDirFaced(),0.9f);

        if (myPlayer.hittable)
        {
            if (myPlayerController.LeftBumper() && canAttack)
            {
                canAttack = false;

                StartCoroutine(AttackAction());
            }
        }
    }
}
