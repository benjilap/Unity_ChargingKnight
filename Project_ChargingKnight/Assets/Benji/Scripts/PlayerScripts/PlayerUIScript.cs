using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIScript : MonoBehaviour {

    [HideInInspector]
    public PlayerClass myPlayer;

    LifeGlobalScript lifeScript;
    KnightClass characClass;

    Text lifeText;
    Scrollbar lifeBar;
    public Scrollbar burstRecover;
    public Scrollbar bashRecover;
    public Scrollbar slashRecover;

    public float[] saveTime = new float[3];

    // Use this for initialization
    void Start () {
        InitUI();
    }
	
	// Update is called once per frame
	void Update () {
        UpdateLifeVisual();
        CheckSkillUse();

        Debug.Log(burstRecover.transform.parent);
        Debug.Log(bashRecover.transform.parent);
        Debug.Log(slashRecover.transform.parent);
    }

    void InitUI()
    {
        if (myPlayer.playerNum == 2) {
            this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
            this.transform.Find("PlayerLifeBar/PlayerLifePoint").GetComponent<RectTransform>().localScale = this.transform.localScale;
            for (int i = 0; i < 3; i++)
            {
                this.transform.Find("SkillPanel").GetChild(i).Find("SkillText").GetComponent<RectTransform>().localScale = this.transform.localScale;
            }
        }
        lifeScript = myPlayer.GetComponent<LifeGlobalScript>();
        characClass = myPlayer.GetComponent<KnightClass>();

        lifeText = this.transform.Find("PlayerLifeBar/PlayerLifePoint").GetComponent<Text>();
        lifeBar = this.transform.Find("PlayerLifeBar/Scrollbar").GetComponent<Scrollbar>();
        slashRecover = this.transform.Find("SkillPanel").GetChild(0).Find("SlashScrollbar").GetComponent<Scrollbar>();
        bashRecover = this.transform.Find("SkillPanel").GetChild(1).Find("BashScrollbar").GetComponent<Scrollbar>();
        burstRecover = this.transform.Find("SkillPanel").GetChild(2).Find("BurstScrollbar").GetComponent<Scrollbar>();


    }

    void UpdateLifeVisual()
    {
        lifeBar.size = lifeScript.lifeValue/100;
        lifeText.text = lifeScript.lifeValue.ToString();
    }


    void CheckSkillUse()
    {
        //BURSTMODE
        if (SaveTimeRecover(characClass.burstModeRecover) != 0)
        {
            saveTime[0] = SaveTimeRecover(characClass.burstModeRecover);
        }
        burstRecover.size = RecoverSkill(saveTime[0], characClass.burstModeRecoverTime);

        //BASHINGSHIELD
        if (SaveTimeRecover(characClass.bashingShieldRecover) != 0)
        {
            saveTime[1] = SaveTimeRecover(characClass.bashingShieldRecover);
        }
        bashRecover.size = RecoverSkill(saveTime[1], characClass.bashingShieldRecoverTime);

        //RAZORSLASH
        if (SaveTimeRecover(characClass.razorSlashRecover) != 0)
        {
            saveTime[2] = SaveTimeRecover(characClass.razorSlashRecover);
        }
        slashRecover.size = RecoverSkill(saveTime[2], characClass.razorSlashRecoverTime);

    }

    float SaveTimeRecover(bool capNoUsed)
    {

        if (!capNoUsed)
        {
            return Time.time;
        }
        else return 0;
    }

    float RecoverSkill(float savedTime, float timeToRecover)
    {

            float recoverTimer = Time.time - savedTime;
            return Mathf.Lerp(0, 1, (recoverTimer / timeToRecover));

    }
}
