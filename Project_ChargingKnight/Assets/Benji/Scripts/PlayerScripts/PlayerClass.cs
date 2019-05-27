using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour {

    [HideInInspector]
    public int playerNum;
    [HideInInspector]
    public bool hittable = true;

    PlayerController playerController;
    Rigidbody playerRb;
    Transform plyrDir;
    Transform cldDir;
    Transform angularInd;

    [SerializeField]
    AnimationCurve accelerationCurve;

    [SerializeField]
    float playerSpeed = 1;
    [SerializeField]
    float playerAcceleration = 1;
    [SerializeField]
    float playerAngularSpeed = 1;
    [SerializeField]
    float playerDodgeAngle = 40;
    [SerializeField]
    float playerDodgeTime = 1;

    Vector3 controllerDir;
    Vector3 dodgeDir;

    bool accelerationState;
    float accelerationTime;
    bool dodgeState;
    float dodgeTime;

    //TempVar
    float tempSpeedPct;
    float tempDecceleration;

    void Start () {
        playerController = this.GetComponent<PlayerController>();
        playerRb = this.GetComponent<Rigidbody>();
        cldDir = this.transform.Find("CtlrDir");
        plyrDir = this.transform.Find("PlyrDir");
        angularInd = this.transform.Find("ControllerDirT");
        //InitAccelCurve();
    }
	
	void Update () {
        if (playerController.CheckControllerNum())
        {
            PlayerMovement();
            PlayerDodgeActivate();
        }
    }

    void PlayerMovement()
    {
        //VisualInfos
        plyrDir.localPosition = playerRb.velocity;

        if (hittable)
        {
            playerRb.velocity = PlayerDir() * playerSpeed;

        }
    }

    void PlayerDodgeActivate()
    {
        if (!dodgeState)
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
        Vector3 tempPlayerDir = Vector3.RotateTowards(playerRb.velocity.normalized, ControllerDir(), playerAngularSpeed * Time.deltaTime, 1f);

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
            print(tempSpeedPct);
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

    
}
