using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Relics : MonoBehaviour {

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


	// Use this for initialization
	void Start () {

        relicManager = GameObject.Find("RelicSpawner");
        transform.eulerAngles = new Vector3(90, 0, 0);
        nombreChasse = Random.Range(0, 5);
        transform.GetChild(nombreChasse).gameObject.SetActive(true);
        //Debug.Log(nombreChasse);
        for (int i = 0; i <= nombreChasse; i++)
        {
            randomInt = Random.Range(0, 5);
            switch (randomInt)
            {
                case 0: //Debug.Log("case 0");
                    if (modAtt == 0 && canModAtt)
                    {
                        modAtt = Random.Range(minRandomMod, maxRandomMod);
                        for (int j = 0; j < transform.GetChild(nombreChasse).childCount; j++)
                        {
                            if (transform.GetChild(nombreChasse).GetChild(j).gameObject.GetComponent<Image>().color == Color.white)
                            {
                                //Debug.Log(transform.GetChild(nombreChasse).GetChild(j).gameObject.GetComponent<Image>().color);
                                transform.GetChild(nombreChasse).GetChild(j).gameObject.GetComponent<Image>().color = new Color(255, 0, 0);
                                if (modAtt < 0)
                                {
                                    Debug.Log(transform.GetChild(nombreChasse).GetChild(j));
                                    transform.GetChild(nombreChasse).GetChild(j).Rotate(new Vector3(0, 0, 180));
                                }

                                break;
                            }


                        }
                        canModAtt = false;
                    }
                    else i--;
                    break;
                case 1:
                    //Debug.Log("case 1");
                    if (modAttSp == 0 && canModAttSp)
                    {
                        modAttSp = Random.Range(minRandomMod, maxRandomMod);
                        for (int j = 0; j < transform.GetChild(nombreChasse).childCount; j++)
                        {
                            if (transform.GetChild(nombreChasse).GetChild(j).gameObject.GetComponent<Image>().color == Color.white)
                            {
                                //Debug.Log(transform.GetChild(nombreChasse).GetChild(j).gameObject.GetComponent<Image>().color);
                                transform.GetChild(nombreChasse).GetChild(j).gameObject.GetComponent<Image>().color = new Color(253, 255, 0);
                                if (modAttSp < 0)
                                {
                                    Debug.Log(transform.GetChild(nombreChasse).GetChild(j));
                                    transform.GetChild(nombreChasse).GetChild(j).Rotate(new Vector3(0, 0, 180));
                                }
                                break;
                            }

                        }
                        canModAttSp = false;
                    }
                    else i--;
                    break;
                case 2:
                    //Debug.Log("case 2");
                    if (modDef == 0 && canModDef)
                    {
                        modDef = Random.Range(minRandomMod, maxRandomMod);
                        for (int j = 0; j < transform.GetChild(nombreChasse).childCount; j++)
                        {
                            if (transform.GetChild(nombreChasse).GetChild(j).gameObject.GetComponent<Image>().color == Color.white)
                            {
                                //Debug.Log(transform.GetChild(nombreChasse).GetChild(j).gameObject.GetComponent<Image>().color);
                                transform.GetChild(nombreChasse).GetChild(j).gameObject.GetComponent<Image>().color = new Color(0, 0, 0);
                                if (modDef < 0)
                                {
                                    Debug.Log(transform.GetChild(nombreChasse).GetChild(j));
                                    transform.GetChild(nombreChasse).GetChild(j).Rotate(new Vector3(0, 0, 180));
                                }
                                break;
                            }

                        }
                        canModDef = false;
                    }
                    else i--;
                    break;
                case 3:
                    //Debug.Log("case 3");
                    if (modSp == 0 && canModSp)
                    {
                        modSp = Random.Range(minRandomMod, maxRandomMod);
                        for (int j = 0; j < transform.GetChild(nombreChasse).childCount; j++)
                        {
                            if (transform.GetChild(nombreChasse).GetChild(j).gameObject.GetComponent<Image>().color == Color.white)
                            {
                                //Debug.Log(transform.GetChild(nombreChasse).GetChild(j).gameObject.GetComponent<Image>().color);
                                transform.GetChild(nombreChasse).GetChild(j).gameObject.GetComponent<Image>().color = new Color(49,255,0);
                                if (modSp < 0)
                                {
                                    Debug.Log(transform.GetChild(nombreChasse).GetChild(j));
                                    transform.GetChild(nombreChasse).GetChild(j).Rotate(new Vector3(0, 0, 180));
                                }
                                break;
                            }
                        }
                        canModSp = false;
                    }
                    else i--;
                    break;
                case 4:
                    //Debug.Log("case 4");
                    if (modAngSp == 0 && canModAngSp)
                    {
                        modAngSp = Random.Range(minRandomMod, maxRandomMod);
                        for (int j = 0; j < transform.GetChild(nombreChasse).childCount; j++)
                        {
                            if (transform.GetChild(nombreChasse).GetChild(j).gameObject.GetComponent<Image>().color == Color.white)
                            {
                                //Debug.Log(transform.GetChild(nombreChasse).GetChild(j).gameObject.GetComponent<Image>().color);
                                transform.GetChild(nombreChasse).GetChild(j).gameObject.GetComponent<Image>().color =  new Color(0, 232, 245);
                                if (modAngSp < 0)
                                {
                                    Debug.Log(transform.GetChild(nombreChasse).GetChild(j));
                                    transform.GetChild(nombreChasse).GetChild(j).Rotate(new Vector3(0, 0, 180));
                                }
                                break;
                            }

                        }
                        canModAngSp = false;
                    }
                    else i--;
                    break;
                case 5:
                    //Debug.Log("case 5");
                    if (modCrit == 0 && canModCrit)
                    {
                        modCrit = Random.Range(minRandomMod, maxRandomMod);
                        for (int j = 0; j < transform.GetChild(nombreChasse).childCount; j++)
                        {
                            if (transform.GetChild(nombreChasse).GetChild(j).gameObject.GetComponent<Image>().color == Color.white)
                            {
                                //Debug.Log(transform.GetChild(nombreChasse).GetChild(j).gameObject.GetComponent<Image>().color);
                                transform.GetChild(nombreChasse).GetChild(j).gameObject.GetComponent<Image>().color = new Color(255, 116, 0);
                                if (modCrit < 0)
                                {
                                    Debug.Log(transform.GetChild(nombreChasse).GetChild(j));
                                    transform.GetChild(nombreChasse).GetChild(j).Rotate(new Vector3(0, 0, 180));
                                }
                                break;
                            }

                        }
                        canModCrit = false;
                    }
                    else i--;
                    break;
                    
            }
            Debug.Log(i);
        }
    }
	// Update is called once per frame
	void Update () {
        player = GameObject.FindGameObjectWithTag("Player");
        if (isTriggered)
        {
            if (player.GetComponent<PlayerController>().RightBumper())
            {
                relicManager.GetComponent<ReliqueManager>().AddRelic(gameObject);
                
            }
        }
	}

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
            isTriggered = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            isTriggered = false;
    }
}


	