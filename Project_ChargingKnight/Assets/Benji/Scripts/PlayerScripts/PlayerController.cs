using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    [HideInInspector]
    public int controllerNum;

    public float HorizontalAxis()
    {
        if (CheckControllerNum())
        {
            return Input.GetAxis("J" + controllerNum + "LeftStickX");
        }
        else
        {
            return 0;
        }
    }

    public float VerticalAxis()
    {
        if (CheckControllerNum())
        {
            return Input.GetAxis("J" + controllerNum + "LeftStickY");
        }
        else
        {
            return 0;
        }
    }

    public bool CheckControllerNum()
    {
        if(controllerNum !=0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
