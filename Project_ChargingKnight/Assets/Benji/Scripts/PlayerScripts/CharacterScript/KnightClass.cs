using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightClass : MonoBehaviour {

    PlayerClass playerClass;
    PlayerCacAttackScript playerAttack;
    AttackTriggerScript playerAtkTrigger;
    Rigidbody playerRb;
    [HideInInspector]
    public bool useSkill;

    //BurstModeValue
    bool burstModeEnable;
    float burstModeDuration = 10;
    int burstModePowerPourcent = 30;
    float burstModePower;

    //BashingShieldValue
    bool bashingShieldEnable;
    float bashingShieldSpeed;
    float bashingShieldKnockback;
    float bashingShieldDuration;
    float bashingShieldDmg;

    // Use this for initialization
    void Start () {
        playerClass = this.GetComponent<PlayerClass>();
        playerAttack = this.GetComponent<PlayerCacAttackScript>();
        playerAtkTrigger = this.transform.Find("AttackZone").GetComponent<AttackTriggerScript>();
        playerRb = this.GetComponent<Rigidbody>();
        burstModePower = 100 / burstModePowerPourcent;

    }
	
	// Update is called once per frame
	void Update () {

	}

    /*void InitPlayerSkill()
    //{
    //    if (playerClass != null)
    //    {
    //        playerClass.ultiSkill += playerClass.UltimateSkill(BurstMode();
    //    }
    }*/


    //BURSTMODE
    public void BurstMode()
    {
        if (!burstModeEnable)
        {
            burstModeEnable = true;
            BurstModeStatsAlteration(playerClass, playerAtkTrigger, 1);
            StartCoroutine(BurstModeCooldown());
        }
    }

    void BurstModeStatsAlteration(PlayerClass plClassToChange, AttackTriggerScript plAtkTrigger, int varModifier)
    {
        plClassToChange.playerSpeed += varModifier * (plClassToChange.playerSpeed * burstModePower);
        plClassToChange.playerAngularSpeed += varModifier * (plClassToChange.playerAngularSpeed * burstModePower);
        plAtkTrigger.hitDamage += varModifier * (plAtkTrigger.hitDamage * burstModePower);
    }

    IEnumerator BurstModeCooldown()
    {
        yield return new WaitForSeconds(burstModeDuration);
        BurstModeStatsAlteration(playerClass, playerAtkTrigger, -1);
        burstModeEnable = false;
    }

    public void BashingShield()
    {
        if (!bashingShieldEnable && !useSkill)
        {
            useSkill = true;
            bashingShieldEnable = true;
            playerRb.velocity = playerRb.velocity.normalized * bashingShieldSpeed;
            playerAtkTrigger.hitDamage = bashingShieldDmg;
            playerAtkTrigger.knokbackPower = bashingShieldKnockback;
            playerAtkTrigger.inBashing = true;

        }
    }

    IEnumerator BashingShieldCooldown()
    {
        yield return new WaitForSeconds(burstModeDuration);
        playerAtkTrigger.hitDamage = playerAttack.AtkDamage;
        playerAtkTrigger.knokbackPower = playerAttack.AtkKnokback;
        playerAtkTrigger.inBashing = false;
        bashingShieldEnable = false;
        useSkill = false;
    }

}
