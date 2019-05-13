using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    float deadValue =0.4f;

    [HideInInspector]
    public int playerNum;

    public float HorizontalAxis()
    {
        if (Input.GetAxis("J" + playerNum + "LeftStickX") > deadValue ||
            Input.GetAxis("J" + playerNum + "LeftStickX") < -deadValue) 
        {
            return Input.GetAxis("J" + playerNum + "LeftStickX");
        }
        else
        {
            return 0;
        }
    }

    public float VerticalAxis()
    {
        if (Input.GetAxis("J" + playerNum + "LeftStickY") > deadValue ||
            Input.GetAxis("J" + playerNum + "LeftStickY") < -deadValue)
        {
            return Input.GetAxis("J" + playerNum + "LeftStickY");
        }
        else
        {
            return 0;
        }
    }

}
