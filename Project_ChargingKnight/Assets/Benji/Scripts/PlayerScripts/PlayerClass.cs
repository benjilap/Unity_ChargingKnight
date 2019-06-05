using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour {

    [HideInInspector]
    public int playerNum;
    [HideInInspector]
    public bool hittable = true;
    [HideInInspector]
    public CameraScript gameCamera;

    PlayerController playerController;
    Rigidbody playerRb;
    Transform plyrDir;
    Transform cldDir;
    Transform angularInd;
    AttackTriggerScript playerAttackZone;


    [SerializeField]
    AnimationCurve accelerationCurve;

    // Alterable Value
    public float playerSpeed = 1;
    public float playerAcceleration = 1;
    public float playerAngularSpeed = 1;
    public float playerDodgeAngle = 40;
    public float playerDodgeTime = 1;
    public float playerAtkDuration = 0.5f;
    public float playerAtkRecover = 0.3f;
    

    LayerMask rayLayerMask;
    Vector3 controllerDir;
    Vector3 dodgeDir;

    bool accelerationState;
    float accelerationTime;
    bool dodgeState;
    float dodgeTime;
    bool canAttack = true;


    //TempVar
    float tempSpeedPct;
    float tempDecceleration;

    void Start () {
        playerController = this.GetComponent<PlayerController>();
        playerRb = this.GetComponent<Rigidbody>();
        cldDir = this.transform.Find("CtlrDir");
        plyrDir = this.transform.Find("PlyrDir");
        angularInd = this.transform.Find("ControllerDirT");
        playerAttackZone = this.transform.Find("AttackZone").GetComponent<AttackTriggerScript>();
        //InitAccelCurve();
    }
	
	void Update () {
        if (playerController.CheckControllerNum())
        {
            PlayerMovement();
            PlayerDodgeActivate();
            PlayerAttack();
        }

        SetLayerMask();
    }

    void PlayerMovement()
    {
        //VisualInfos
        plyrDir.localPosition = playerRb.velocity;

        if (hittable )
        {

            playerRb.velocity = PlayerDir() * playerSpeed;

        }

        if(BounceObstacle() != Vector3.zero)
        {
            playerRb.velocity = Vector3.Reflect(playerRb.velocity.normalized, BounceObstacle()) * playerSpeed * AccelerationSpeed();
        }
    }

    void PlayerDodgeActivate()
    {
        if (!dodgeState&&canAttack)
        {
            if (playerController.ButtonA())
            {
                dodgeState = true;
                dodgeTime = Time.time;
                hittable = false;
                dodgeDir = ControllerDir();
            }
        }
        if (!hittable)
        {
            float dodgeTimer = Time.time - dodgeTime;
            if (dodgeTimer >= playerDodgeTime)
            {
                if (playerRb.velocity.magnitude / playerSpeed <= 0.5f)
                {
                    hittable = true;
                    accelerationState = false;
                }
                else
                {
                    float tempTime = playerDodgeTime - (dodgeTimer - playerDodgeTime / 2) * (1 / playerAcceleration);
                    playerRb.velocity = dodgeDir * accelerationCurve.Evaluate(tempTime) * playerSpeed;
                }
            }
            else if (dodgeTimer >= playerDodgeTime / 2)
            {
                float tempTime = playerDodgeTime - (dodgeTimer - playerDodgeTime / 2) * (1 / playerAcceleration);
                playerRb.velocity = dodgeDir * accelerationCurve.Evaluate(tempTime) * playerSpeed;
            }
            else
            {
                playerRb.velocity = dodgeDir  * playerSpeed;

            }
        }
    }

    Vector3 ControllerDir()
    {
        Vector3 tempControllerDir = new Vector3(playerController.HorizontalAxis(), 0, playerController.VerticalAxis()).normalized;
        if ( tempControllerDir.magnitude != 0)
        {
            controllerDir = tempControllerDir;
            return controllerDir;
        }
        else
        {
            if(controllerDir == Vector3.zero)
            {
                controllerDir = Vector3.forward;
            }
            else
            {
                controllerDir = playerRb.velocity.normalized;
            }
            return controllerDir;
        }

    }

    Vector3 PlayerDir()
    {
        Vector3 tempPlayerDir = Vector3.zero;

        tempPlayerDir = Vector3.RotateTowards(playerRb.velocity.normalized, ControllerDir(), playerAngularSpeed * Time.deltaTime, 1f);

        //VisualInfos
        cldDir.localPosition = tempPlayerDir;

        return tempPlayerDir * AccelerationSpeed();
    }

    float AccelerationSpeed()
    {

        if(!accelerationState)
        {
            accelerationState = true;
            accelerationTime = Time.time;
            tempSpeedPct = playerRb.velocity.magnitude / playerSpeed;
        }

        float tempTime = Mathf.Clamp01((Time.time - accelerationTime) * (1 / playerAcceleration) + tempSpeedPct);
        if (dodgeState && playerRb.velocity.magnitude >= playerSpeed)
        {
            dodgeState = false;
        }
        return accelerationCurve.Evaluate(tempTime);
    }

    void InitAccelCurve()
    {
        accelerationCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.4f, 0.6f), new Keyframe(1, 1));

    }

    void PlayerAttack()
    {
        UpdateAttackZoneTrans();

        if (hittable)
        {
            if (playerController.LeftBumper() && canAttack)
            {
                canAttack = false;
                playerAttackZone.playerIsAttacking = true;

                StartCoroutine(ResetAttack());
            }
        }
    }
    
    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(playerAtkDuration);
        playerAttackZone.playerIsAttacking = false;
        yield return new WaitForSeconds(playerAtkRecover);
        canAttack = true;

    }

    public int PlayerDirFaced()
    {
        if (playerRb.velocity.normalized.x > 0.5f)
        {
            return 1;

        }
        else if (playerRb.velocity.normalized.x < -0.5f)
        {
            return 3;

        }
        else if (playerRb.velocity.normalized.z >= 0.5f)
        {
            return 2;

        }
        else if (playerRb.velocity.normalized.z <= 0.5f)
        {
            return 0;

        }
        else
        {
            return 0;
        }
    }

    void UpdateAttackZoneTrans()
    {
        if (PlayerDirFaced() == 0)
        {
            playerAttackZone.transform.localPosition = new Vector3(0, 0, -0.75f);
            playerAttackZone.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        if(PlayerDirFaced() == 1)
        {
            playerAttackZone.transform.localPosition = new Vector3(0.75f, 0, 0);
            playerAttackZone.transform.eulerAngles = new Vector3(0, 90, 0);
        }
        else
        if(PlayerDirFaced() == 2)
        {
            playerAttackZone.transform.localPosition = new Vector3(0, 0, 0.75f);
            playerAttackZone.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        if(PlayerDirFaced() == 3)
        {
            playerAttackZone.transform.localPosition = new Vector3(-0.75f, 0, 0);
            playerAttackZone.transform.eulerAngles = new Vector3(0, 90, 0);
        }
    }

    Vector3 BounceObstacle()
    {
        Vector3 minViewportWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 9.25f));
        Vector3 maxViewportWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 9.25f));
        Vector3 playerNextDir = this.transform.position + playerRb.velocity.normalized * 0.5f;

        Ray fwdRay = new Ray(this.transform.position, playerRb.velocity.normalized);
        RaycastHit hit;

        Debug.DrawLine(this.transform.position, this.transform.position + playerRb.velocity.normalized * 1, Color.red);

        if (Physics.Raycast(fwdRay, out hit, 1, rayLayerMask))
        {
            return hit.normal;
        }
        else

        if (playerNextDir.x >= maxViewportWorldPos.x)
        {
            return Vector3.left;
        }
        else
        if (playerNextDir.x <= minViewportWorldPos.x)
        {
            return Vector3.right;
        }
        else

        if (playerNextDir.z >=  maxViewportWorldPos.z)
        {
            return Vector3.back;
        }
        else
        if (playerNextDir.z <= minViewportWorldPos.z)
        {
            return Vector3.forward;
        }
        else
        {
            return Vector3.zero;
        }
    }

    void SetLayerMask()
    {
        if (this.gameObject.layer != LayerMask.NameToLayer(this.name))
        {
            for (int i = 0; i < 32; i++)
            {

                if (LayerMask.LayerToName(i) != "")
                {
                    if (LayerMask.LayerToName(i) == this.gameObject.name)
                    {
                        rayLayerMask.value |= 0 << i;
                        this.gameObject.layer = i;

                    }
                    else if (LayerMask.LayerToName(i) == "MinimapRender")
                    {
                        rayLayerMask.value |= 0 << i;
                    }
                    else
                    {
                        rayLayerMask.value |= 1 << i;

                    }
                }
            }
            for(int i = 0; i < this.transform.childCount; i++)
            {
                if(this.transform.GetChild(i).gameObject.layer == LayerMask.NameToLayer("Default"))
                {
                    this.transform.GetChild(i).gameObject.layer = this.gameObject.layer;
                }
            }
        }
    }
}
