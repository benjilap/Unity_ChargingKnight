using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour {

    PlayerController playerController;
    Rigidbody playerRb;

    [SerializeField]
    float playerSpeed = 1;

	void Start () {
        playerController = this.GetComponent<PlayerController>();
        playerRb = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        PlayerMovement();
	}

    void PlayerMovement()
    {
        playerRb.velocity = new Vector3(playerController.HorizontalAxis(), 0, -playerController.VerticalAxis()).normalized * playerSpeed;
    }
}
