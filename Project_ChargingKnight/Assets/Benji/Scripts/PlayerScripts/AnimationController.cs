using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {

    PlayerClass playerClss;
    Animator myAtor;

	void Start () {
        playerClss = this.transform.parent.GetComponent<PlayerClass>();
        myAtor = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateDirFaced();
	}

    void UpdateDirFaced()
    {
        myAtor.SetInteger("PlayerDirFaced", playerClss.PlayerDirFaced());
    } 
}
