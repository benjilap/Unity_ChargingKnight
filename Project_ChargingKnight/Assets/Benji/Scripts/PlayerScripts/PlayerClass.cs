using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour {

    [HideInInspector]
    public int playerNum;
    
    PlayerController playerController;
    Rigidbody playerRb;
    Transform aimDir;
    Transform plyrDir;
    Transform cldDir;

    [SerializeField]
    AnimationCurve accelerationCurve;

    [SerializeField]
    float playerSpeed = 1;
    [SerializeField]
    float playerAcceleration = 1;
    [SerializeField]
    float playerAngularSpeed = 1;

    Vector3 controllerDir;

    bool accelerationState;
    float accelerationTime;
    bool angularMoveState;
    float angularMoveTime;

    void Start () {
        playerController = this.GetComponent<PlayerController>();
        playerRb = this.GetComponent<Rigidbody>();
        aimDir = this.transform.Find("CtlrDir");
        cldDir = this.transform.Find("CltdDir");
        plyrDir = this.transform.Find("PlyrDir");
        //InitAccelCurve();
    }
	
	// Update is called once per frame
	void Update () {
        if (playerController.CheckControllerNum())
        {
            PlayerMovement();
        }

        
    }

    void PlayerMovement()
    {
        aimDir.localPosition = new Vector3(playerController.HorizontalAxis(), 0, playerController.VerticalAxis()).normalized;
        plyrDir.localPosition = playerRb.velocity;

        playerRb.velocity = PlayerDir() * playerSpeed;
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
        cldDir.localPosition = tempPlayerDir;
        Debug.Log(tempPlayerDir);

        return tempPlayerDir * AccelerationSpeed();
    }

    float AccelerationSpeed()
    {
        if(!accelerationState)
        {
            accelerationState = true;
            accelerationTime = Time.time;
        }

        float tempTime = Mathf.Clamp01((Time.time - accelerationTime) * (1 / playerAcceleration));

        return accelerationCurve.Evaluate(tempTime);
    }

    void InitAccelCurve()
    {
        accelerationCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.4f, 0.6f), new Keyframe(1, 1));

    }
}
