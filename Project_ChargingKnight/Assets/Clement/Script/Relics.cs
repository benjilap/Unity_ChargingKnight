using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Relics : MonoBehaviour {
    GameObject clone;
    GameObject hud;
    GameObject player;
    GameObject relicManager;
    public float modAtt = 0;
    public float modAttSp = 0;
    public float modDef = 0;
    public float modSp = 0;
    public float modAngSp = 0;
    public float modCrit = 0;
    public int nombreChasse;
    int randomInt;
    [SerializeField]
    float minRandomMod;
    [SerializeField]
    float maxRandomMod;
    bool canModAtt = true;
    bool canModAttSp = true;
    bool canModDef = true;
    bool canModSp = true;
    bool canModAngSp = true;
    bool canModCrit = true;
    bool isTriggered;
    [SerializeField]
    Sprite mAtt;
    [SerializeField]
    Sprite mDef;
    [SerializeField]
    Sprite mAttSp;
    [SerializeField]
    Sprite mSp;
    [SerializeField]
    Sprite mAngSp;
    [SerializeField]
    Sprite mCrit;
    [SerializeField]
    Sprite bAtt;
    [SerializeField]
    Sprite bDef;
    [SerializeField]
    Sprite bAttSp;
    [SerializeField]
    Sprite bSp;
    [SerializeField]
    Sprite bAngSp;
    [SerializeField]
    Sprite bCrit;
    [SerializeField]
    Sprite triangle;

    // Use this for initialization
    void Start () {
        hud = GameObject.Find("RelicRender");
        relicManager = GameObject.Find("RelicSpawner");
        transform.eulerAngles = new Vector3(90, 0, 0);
        nombreChasse = Random.Range(0, 5);
        transform.GetChild(nombreChasse).gameObject.SetActive(true);
        ////Debug.Log(nombreChasse);
        for (int i = 0; i <= nombreChasse; i++)
        {
            randomInt = Random.Range(0, 5);
            switch (randomInt)
            {                
                case 0: 
                    if (modAtt == 0 && canModAtt)
                    {
                        modAtt = Random.Range(minRandomMod, maxRandomMod);
                        for (int j = 0; j < transform.GetChild(nombreChasse).childCount; j++)
                        {
                            if (transform.GetChild(nombreChasse).GetChild(j).gameObject.GetComponent<Image>().sprite == null)
                            {                               
                                if (modAtt < 0)
                                {
                                    transform.GetChild(nombreChasse).GetChild(j).gameObject.GetComponent<Image>().sprite = mAtt;
                                }
                                else if(modAtt>0)
                                    transform.GetChild(nombreChasse).GetChild(j).gameObject.GetComponent<Image>().sprite = bAtt;
                                break;
                            }
                        }
                        canModAtt = false;
                    }
                    else i--;
                    break;
                case 1:
                    if (modAttSp == 0 && canModAttSp)
                    {
                        modAttSp = Random.Range(minRandomMod, maxRandomMod);
                        for (int j = 0; j < transform.GetChild(nombreChasse).childCount; j++)
                        {
                            if (transform.GetChild(nombreChasse).GetChild(j).gameObject.GetComponent<Image>().sprite == null)
                            {                                
                                if (modAttSp < 0)
                                {
                                    transform.GetChild(nombreChasse).GetChild(j).gameObject.GetComponent<Image>().sprite = mAttSp;
                                }
                                else if(modAttSp >0)
                                    transform.GetChild(nombreChasse).GetChild(j).gameObject.GetComponent<Image>().sprite = bAttSp;
                                break;
                            }
                        }
                        canModAttSp = false;
                    }
                    else i--;
                    break;
                case 2:
                    if (modDef == 0 && canModDef)
                    {
                        modDef = Random.Range(minRandomMod, maxRandomMod);
                        for (int j = 0; j < transform.GetChild(nombreChasse).childCount; j++)
                        {
                            if (transform.GetChild(nombreChasse).GetChild(j).gameObject.GetComponent<Image>().sprite == null)
                            {
                                if (modDef < 0)
                                {
                                    transform.GetChild(nombreChasse).GetChild(j).gameObject.GetComponent<Image>().sprite = mDef;
                                }
                                else if(modDef > 0)
                                    transform.GetChild(nombreChasse).GetChild(j).gameObject.GetComponent<Image>().sprite = bDef;
                                break;
                            }
                        }
                        canModDef = false;
                    }
                    else i--;
                    break;
                case 3:
                    if (modSp == 0 && canModSp)
                    {
                        modSp = Random.Range(minRandomMod, maxRandomMod);
                        for (int j = 0; j < transform.GetChild(nombreChasse).childCount; j++)
                        {
                            if (transform.GetChild(nombreChasse).GetChild(j).gameObject.GetComponent<Image>().sprite == null)
                            { 
                                if (modSp < 0)
                                {
                                    transform.GetChild(nombreChasse).GetChild(j).gameObject.GetComponent<Image>().sprite = mSp;
                                }
                                else if(modSp > 0)
                                    transform.GetChild(nombreChasse).GetChild(j).gameObject.GetComponent<Image>().sprite = bSp;
                                break;
                            }
                        }
                        canModSp = false;
                    }
                    else i--;
                    break;
                case 4:
                    if (modAngSp == 0 && canModAngSp)
                    {
                        modAngSp = Random.Range(minRandomMod, maxRandomMod);
                        for (int j = 0; j < transform.GetChild(nombreChasse).childCount; j++)
                        {
                            if (transform.GetChild(nombreChasse).GetChild(j).gameObject.GetComponent<Image>().sprite == null)
                            {
                                if (modAngSp < 0)
                                {
                                    transform.GetChild(nombreChasse).GetChild(j).gameObject.GetComponent<Image>().sprite = mAngSp;
                                }
                                else if(modAngSp>0)
                                    transform.GetChild(nombreChasse).GetChild(j).gameObject.GetComponent<Image>().sprite = bAngSp;
                            }
                        }
                        canModAngSp = false;
                    }
                    else i--;
                    break;
                case 5:
                    if (modCrit == 0 && canModCrit)
                    {
                        modCrit = Random.Range(minRandomMod, maxRandomMod);
                        for (int j = 0; j < transform.GetChild(nombreChasse).childCount; j++)
                        {
                            if (transform.GetChild(nombreChasse).GetChild(j).gameObject.GetComponent<Image>().sprite == null)
                            {
                                if (modCrit < 0)
                                {
                                    transform.GetChild(nombreChasse).GetChild(j).gameObject.GetComponent<Image>().sprite = mCrit;
                                }
                                else if(modCrit >0)
                                    transform.GetChild(nombreChasse).GetChild(j).gameObject.GetComponent<Image>().sprite = bCrit;
                                break;
                            }
                        }
                        canModCrit = false;
                    }
                    else i--;
                    break;
                    
            }
        }
    }
	// Update is called once per frame
	void Update () {
        player = GameObject.FindGameObjectWithTag("Player");
        if (Input.GetKeyDown("c"))
        {
            relicManager.GetComponent<ReliqueManager>().AddRelic(gameObject);
            transform.localEulerAngles.Set(0, 0, 0);
            transform.localScale.Set(1, 1, 1);
            if (hud.transform.childCount != 0)
            {
                for (int i = 0; i < hud.transform.childCount; i++)
                {
                    Destroy(hud.transform.GetChild(i).gameObject);
                }
            }
        }
        if (isTriggered)
        {

            if (player.GetComponent<PlayerController>().RightBumper())
            {
                Debug.Log("Jajj");
                relicManager.GetComponent<ReliqueManager>().AddRelic(gameObject);
                transform.localEulerAngles.Set(0, 0, 0);
                transform.localScale.Set(1, 1, 1);
                if (hud.transform.childCount != 0)
                {
                    for (int i = 0; i < hud.transform.childCount; i++)
                    {
                        Destroy(hud.transform.GetChild(i).gameObject);
                    }
                }
            }
        }
	}

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {

            isTriggered = true;
            if (hud.transform.childCount == 0)
            {
                for(int i = 0; i <=nombreChasse ; i++)
                clone = Instantiate(gameObject.transform.GetChild(nombreChasse).GetChild(i).gameObject, hud.transform);
                clone.transform.localEulerAngles.Set(0, 0, 0);
                clone.transform.localPosition.Set(0, 0, 0);
                hud.transform.localScale = new Vector3(4, 4, 4);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isTriggered = false;
            if (hud.transform.childCount !=0)
            {
                for(int i =0; i<hud.transform.childCount; i++)
                {
                    Destroy(hud.transform.GetChild(i).gameObject);
                }
            }
        }
    }
}


	