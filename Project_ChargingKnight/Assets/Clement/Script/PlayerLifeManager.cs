using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeManager : MonoBehaviour {

    public float life;
    int degats;
    Slider barVie;
    float sliderValue;

	// Use this for initialization
	void Start () {
        //barVie = Slider.Fin("BarVie");
     barVie.maxValue = life;
     sliderValue = life;
     barVie.value = sliderValue;
        System.Math.Round(life, 2);
            }
	
	// Update is called once per frame
	void Update () {
        
	}

    private void OnHit()
    {
            //degats = collision.gameObject.GetComponent<Script>.degats;
            sliderValue = barVie.value;
            life = life - degats;
            sliderValue.Equals(life);    
    }
}
