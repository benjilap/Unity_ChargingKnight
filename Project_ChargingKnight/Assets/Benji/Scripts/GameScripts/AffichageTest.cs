using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AffichageTest : MonoBehaviour {

    public LifeGlobalScript LifeAffichage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Find("LifeText").GetComponent<Text>().text = LifeAffichage.lifeValue.ToString();
	}
}
