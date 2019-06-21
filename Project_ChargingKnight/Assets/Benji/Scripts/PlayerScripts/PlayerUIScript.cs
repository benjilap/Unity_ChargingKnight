using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIScript : MonoBehaviour {

    [HideInInspector]
    public PlayerClass myPlayer;

    Scrollbar lifeBar;
    Scrollbar burstRecover;
    Scrollbar bashRecover;
    Scrollbar slashRecover;

    // Use this for initialization
    void Start () {
        InitUI();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void InitUI()
    {
        if (myPlayer.playerNum == 2) {
            this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
            this.transform.Find("PlayerLifeBar/PlayerLifePoint").GetComponent<RectTransform>().localScale = this.transform.localScale;
        }

        lifeBar = this.transform.Find("PlayerLifeBar/Scrollbar").GetComponent<Scrollbar>();
        burstRecover = this.transform.Find("SkillPanel/BurstModeRecover/Scrollbar").GetComponent<Scrollbar>();
        bashRecover = this.transform.Find("SkillPanel/BashingShieldRecover/Scrollbar").GetComponent<Scrollbar>();
        slashRecover = this.transform.Find("SkillPanel/RazorSlashRecover/Scrollbar").GetComponent<Scrollbar>();
    }
}
