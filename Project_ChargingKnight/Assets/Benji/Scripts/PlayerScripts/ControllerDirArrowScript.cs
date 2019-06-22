using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerDirArrowScript : MonoBehaviour {

    Rigidbody playerRb;
    PlayerClass myPlayer;
    PlayerController myController;
    SpriteRenderer playerSprite;
    SpriteRenderer myArrowSprite;
    Vector3 lastControllerInput = Vector3.forward;


	void Start () {
        playerRb = this.transform.parent.GetComponent<Rigidbody>();
        myController = this.transform.parent.GetComponent<PlayerController>();
        myPlayer = this.transform.parent.GetComponent<PlayerClass>();
        playerSprite = this.transform.parent.Find("PlayerSprite").GetComponent<SpriteRenderer>();
        myArrowSprite = this.transform.GetChild(0).GetComponent<SpriteRenderer>();

    }
	
	// Update is called once per frame
	void Update () {
        SetArrowColor();
        SetArrowTransform();
	}

    void SetArrowTransform()
    {
        Vector3 controllerDir = new Vector3(myController.HorizontalAxis(), 0, myController.VerticalAxis()).normalized;
        if(controllerDir.magnitude == 0)
        {
            controllerDir = lastControllerInput;
        }
        else
        {
            lastControllerInput = controllerDir;
        }

        this.transform.localPosition = controllerDir;
        this.transform.rotation = Quaternion.LookRotation(controllerDir*2);
        if (this.transform.position.z > this.transform.parent.position.z)
        {
            myArrowSprite.sortingOrder = playerSprite.sortingOrder - 5;
        }
        else
        {
            myArrowSprite.sortingOrder = playerSprite.sortingOrder + 5;

        }
    }

    void SetArrowColor()
    {
        myArrowSprite.color = GameManager.SetPlayerColor(myPlayer.playerNum);
    }
}
