using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeGlobalScript : MonoBehaviour {

    public float lifeValue;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CheckDeath();
	}

    void CheckDeath()
    {
        if (lifeValue <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
