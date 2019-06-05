using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCacScript : EnemyScript {

	// Use this for initialization
	void Start () {
        InitLayerMask();
        InitVar();
        UpdateAngleLimit();
	}
	
	// Update is called once per frame
	void Update () {
        FovRadar();
	}
}
