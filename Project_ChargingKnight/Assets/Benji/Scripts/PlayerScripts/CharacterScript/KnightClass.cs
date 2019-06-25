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
    public float burstModeRecoverTime =1;
    [HideInInspector]
    public bool burstModeRecover;

    //BashingShieldValue
    [HideInInspector]
    public bool bashingShieldEnable;
    float bashingShieldSpeed = 20;
    float bashingShieldKnockback = 20;
    float bashingShieldDuration = 1;
    float bashingShieldDmg = 5;
    public float bashingShieldRecoverTime =1;
    [HideInInspector]
    public bool bashingShieldRecover;

    //RazorSlash
    [HideInInspector]
    public bool razorSlashEnable;
    float razorSlashSpeed = 20;
    float razorSlashKnockback = 20;
    float razorSlashDuration = 1;
    float razorSlashDmg = 5;
    public float razorSlashRecoverTime =1;
    [HideInInspector]
    public bool razorSlashRecover;
    Vector3 razorSlashVelocityRecover;

    // Use this for initialization
    void Start () {
        playerClass = this.GetComponent<PlayerClass>();
        playerAttack = this.GetComponent<PlayerCacAttackScript>();
        playerAtkTrigger = this.transform.Find("AttackZone").GetComponent<AttackTriggerScript>();
        playerRb = this.GetComponent<Rigidbody>();
        playerController = this.GetComponent<PlayerController>();
        burstModePower = burstModePowerPourcent/100;
        burstModeRecoverTime += burstModeDuration;
        bashingShieldRecoverTime += bashingShieldDuration;
        razorSlashRecoverTime += razorSlashDuration;
        InitPlayerSkill();

    }
	
	// Update is called once per frame
	void Update () {
        if (bashingShieldEnable)
        {
            BashingShield();
        }
        Debug.DrawRay(this.transform.position,RazorSlashDir(), Color.red);
        //Debug.Log(RazorSlashDir());
    }

    void InitPlayerSkill()
    {
        if (playerClass != null)
        {
            playerClass.primarySkill = RazorSlash;
            playerClass.secondarySkill = BashingShield;
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
                StartCoroutine(BurstModeRecover());

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
            StartCoroutine(BashingShieldRecover());

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
    }
    IEnumerator BashingShieldRecover()
    {
        bashingShieldRecover = true;
        yield return new WaitForSeconds(bashingShieldRecoverTime);
        bashingShieldRecover = false;

    }

    //RAZORSLASH
    public void RazorSlash()
    {
        if (!razorSlashEnable && !useSkill)
        {
            useSkill = true;
            razorSlashEnable = true;
            razorSlashVelocityRecover = playerRb.velocity.normalized;
            RazorSlashUpdateAtkZone(playerClass.PlayerDirFaced(), 0.9f);
            playerRb.velocity = RazorSlashDir() * razorSlashSpeed;
            playerAtkTrigger.hitDamage = razorSlashDmg;
            playerAtkTrigger.knokbackPower = razorSlashKnockback;
            playerAtkTrigger.inRazor = true;
            playerAtkTrigger.isAttacking = true;
            StartCoroutine(RazorSlashCooldown());
            StartCoroutine(RazorSlashRecover());

        }
        //playerRb.velocity = playerRb.velocity.normalized * razorSlashSpeed;
    }

    Vector3 RazorSlashDir()
    {
        Vector3 controllerDir = new Vector3(playerController.HorizontalAxis(), 0, playerController.VerticalAxis()).normalized;
        Vector3 playerDir = playerRb.velocity.normalized;
        float angleToControllerDir = AngleTo(Vector3.forward, controllerDir);
        float angleToPlayerDir = AngleTo(Vector3.forward, playerDir);

        if (angleToControllerDir < angleToPlayerDir)
        {
            return new Vector3(Mathf.Sin((angleToPlayerDir - 90)*Mathf.Deg2Rad), 0, Mathf.Cos((angleToPlayerDir - 90) * Mathf.Deg2Rad));
        }
        else
        {
            return new Vector3(Mathf.Sin((angleToPlayerDir + 90) * Mathf.Deg2Rad), 0, Mathf.Cos((angleToPlayerDir + 90) * Mathf.Deg2Rad));
            
        }
    }

    void RazorSlashUpdateAtkZone(int dirFaced, float zonePos)
    {

        if (dirFaced == 0)
        {
            playerAtkTrigger.transform.localPosition = new Vector3(0, 0, -zonePos);
            playerAtkTrigger.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        if (dirFaced == 1)
        {
            playerAtkTrigger.transform.localPosition = new Vector3(zonePos, 0, 0);
            playerAtkTrigger.transform.eulerAngles = new Vector3(0, 90, 0);
        }
        else
        if (dirFaced == 2)
        {
            playerAtkTrigger.transform.localPosition = new Vector3(0, 0, zonePos);
            playerAtkTrigger.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        if (dirFaced == 3)
        {
            playerAtkTrigger.transform.localPosition = new Vector3(-zonePos, 0, 0);
            playerAtkTrigger.transform.eulerAngles = new Vector3(0, 90, 0);
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



    IEnumerator RazorSlashCooldown()
    {
        yield return new WaitForSeconds(razorSlashDuration);
        playerAtkTrigger.hitDamage = playerAttack.AtkDamage;
        playerAtkTrigger.knokbackPower = playerAttack.AtkKnokback;
        playerAtkTrigger.isAttacking = false;
        playerAtkTrigger.inRazor = false;
        razorSlashEnable = false;
        useSkill = false;
        playerRb.velocity = razorSlashVelocityRecover * playerClass.playerSpeed;
    }
    IEnumerator RazorSlashRecover()
    {
        razorSlashRecover = true;
        yield return new WaitForSeconds(razorSlashRecoverTime);
        razorSlashRecover = false;

    }

}
