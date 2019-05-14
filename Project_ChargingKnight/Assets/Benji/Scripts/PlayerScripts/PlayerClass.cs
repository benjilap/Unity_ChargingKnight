using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour {

    PlayerController playerController;
    Rigidbody playerRb;
    Transform aimDir;

    [SerializeField]
    float playerSpeed = 1;

    Vector3 playerDir;

	void Start () {
        playerController = this.GetComponent<PlayerController>();
        playerRb = this.GetComponent<Rigidbody>();
        aimDir = this.transform.Find("DirInd");
	}
	
	// Update is called once per frame
	void Update () {
        PlayerMovement();
	}

    void PlayerMovement()
    {
        playerDir = new Vector3(playerController.HorizontalAxis(), 0, -playerController.VerticalAxis()).normalized;
        aimDir.localPosition = playerDir;

        playerRb.velocity = playerDir * playerSpeed;
    }
}
