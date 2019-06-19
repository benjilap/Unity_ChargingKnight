using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightClass : MonoBehaviour {

    PlayerClass playerClass;
    PlayerCacAttackScript playerAttack;
    AttackTriggerScript playerAtkTrigger;
    PlayerController playerController;
    Rigidbody playerRb;
    [HideInInspector]
    public bool useSkill;

    //BurstModeValue
    bool burstModeEnable;
    float burstModeDuration = 10;
    float burstModePowerPourcent = 30;
    [SerializeField]
    float burstModePower;
    [SerializeField]
    float burstModeRecoverTime;
    bool burstModeRecover;

    //BashingShieldValue
    [HideInInspector]
    public bool bashingShieldEnable;
    float bashingShieldSpeed = 20;
    float bashingShieldKnockback = 20;
    float bashingShieldDuration = 1;
    float bashingShieldDmg = 5;
    [SerializeField]
    float bashingShieldRecoverTime;
    bool bashingShieldRecover;

    // Use this for initialization
    void Start () {
        playerClass = this.GetComponent<PlayerClass>();
        playerAttack = this.GetComponent<PlayerCacAttackScript>();
        playerAtkTrigger = this.transform.Find("AttackZone").GetComponent<AttackTriggerScript>();
        playerRb = this.GetComponent<Rigidbody>();
        playerController = this.GetComponent<PlayerController>();
        burstModePower = burstModePowerPourcent/100;

    }
	
	// Update is called once per frame
	void Update () {
        if (bashingShieldEnable)
        {
            BashingShield();
        }
        Debug.DrawRay(this.transform.position, RazorSlashDir(),Color.red);
        Debug.Log(RazorSlashDir());
	}

    void InitPlayerSkill()
    {
        if (playerClass != null)
        {
            playerClass.ultiSkill = BurstMode;
        }
    }


    //BURSTMODE
    public void BurstMode()
    {
        if (!burstModeRecover)
        {
            if (!burstModeEnable)
            {
                burstModeEnable = true;
                BurstModeStatsAlteration(playerClass, playerAtkTrigger, 1);
                StartCoroutine(BurstModeCooldown());
            }

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
        StartCoroutine(BurstModeRecover());
    }
    IEnumerator BurstModeRecover()
    {
        burstModeRecover = true;
        yield return new WaitForSeconds(burstModeRecoverTime);
        burstModeRecover = false;

    }

    //BASHINGSHIELD
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
            playerAtkTrigger.isAttacking = true;
            StartCoroutine(BashingShieldCooldown());
        }
            playerRb.velocity = playerRb.velocity.normalized * bashingShieldSpeed;

    }

    IEnumerator BashingShieldCooldown()
    {
        yield return new WaitForSeconds(bashingShieldDuration);
        playerAtkTrigger.hitDamage = playerAttack.AtkDamage;
        playerAtkTrigger.knokbackPower = playerAttack.AtkKnokback;
        playerAtkTrigger.isAttacking = false;
        playerAtkTrigger.inBashing = false;
        bashingShieldEnable = false;
        useSkill = false;
        StartCoroutine(BashingShieldRecover());
    }
    IEnumerator BashingShieldRecover()
    {
        bashingShieldRecover = true;
        yield return new WaitForSeconds(bashingShieldRecoverTime);
        bashingShieldRecover = false;

    }

    //RAZORSLASH
    public Vector3 RazorSlashDir()
    {
        Vector3 controllerDir = new Vector3(playerController.HorizontalAxis(), 0, playerController.VerticalAxis()).normalized;
        Vector3 playerDir = playerRb.velocity.normalized;
        float angleToControllerDir = AngleTo(Vector3.forward, controllerDir);
        float angleToPlayerDir = AngleTo(Vector3.forward, playerDir);

        if (angleToControllerDir < angleToPlayerDir)
        {
            return new Vector3(Mathf.Sin(angleToPlayerDir - 90), 0, Mathf.Cos(angleToPlayerDir - 90));
        }
        else
        {
            return new Vector3(Mathf.Sin(angleToPlayerDir + 90), 0, Mathf.Cos(angleToPlayerDir +90));


        }

    }

    private float AngleTo(Vector3 pos, Vector3 target)
    {
        float angle = 0;

        if (target.x > pos.x)
            angle = Vector3.Angle(target, pos);
        else
            angle = 360 - Vector3.Angle(target, pos);

        return angle;
    }

}
