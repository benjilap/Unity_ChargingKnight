using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReliqueManager : MonoBehaviour
{
    GameObject manager;
    public int nombreSlots;
    List<GameObject> relicsExistantes = new List<GameObject>();
    public List<GameObject> relicsEquip = new List<GameObject>();
    Object relicPrefab;
    GameObject player;
    GameObject lastCreated;
    public float coefAtt;
    public float coefAttSp;
    public float coefDef;
    public float coefSp;
    public float coefAngSp;
    public float coefCrit;
    public float crit = 5; //Temporaire (pour la demo)
    public float def = 10; //Temporaire (pour la demo)
    float initAtt;
    float initAttSp;
    float initDef = 10;
    float initSp;
    float initAngSp;
    float initCrit = 5;

    bool initDone;

    // Use this for initialization
    void Start()
    {
        manager = GameObject.Find("GM_Canvas");
        relicPrefab = Resources.Load("Relics/Relic");
        player = GameObject.Find("Player1");
        

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            relicsEquip.Clear();
            StatModifier();
        }

    }

    void InitVar()
    {
        if (!initDone &&player != null)
        {
            initAtt = player.GetComponent<PlayerCacAttackScript>().AtkDamage;
            initAttSp = player.GetComponent<PlayerCacAttackScript>().AtkRecover;
            initSp = player.GetComponent<PlayerClass>().playerSpeed;
            initAngSp = player.GetComponent<PlayerClass>().playerAngularSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.name == "Player1" || other.gameObject.name == "Player2"))
            SpawnRelic(transform.position);

    }

    public void AddRelic(GameObject anotherOne)
    {
        if (relicsEquip.Count < nombreSlots)
        {
            relicsEquip.Add(anotherOne);
            manager.GetComponent<Pause>().RelicRender(relicsEquip);
            StatModifier();
        }
    }

    void SpawnRelic(Vector3 posi)
    {

        if (posi != null)
            lastCreated = Instantiate(relicPrefab, posi - new Vector3(0,0,2), new Quaternion(0, 0, 0, 0)) as GameObject;
        else lastCreated = Instantiate(relicPrefab, player.transform.position, new Quaternion(0, 0, 0, 0)) as GameObject;       

    }

    public void StatModifier()
    {
        for(int i = 0; i < relicsEquip.Count; i++)
        {
            coefAtt += relicsEquip[i].GetComponent<Relics>().modAtt;
            coefAttSp += relicsEquip[i].GetComponent<Relics>().modAttSp;
            coefDef += relicsEquip[i].GetComponent<Relics>().modDef;
            coefSp += relicsEquip[i].GetComponent<Relics>().modSp;
            coefAngSp += relicsEquip[i].GetComponent<Relics>().modAngSp;
            coefCrit += relicsEquip[i].GetComponent<Relics>().modCrit;
        }
        player.GetComponent<PlayerCacAttackScript>().AtkDamage = initAtt * (1+coefAtt);
        player.GetComponent<PlayerCacAttackScript>().AtkRecover = initAttSp * (1+coefAttSp);
        def = initDef * (1+coefDef);
        initSp = player.GetComponent<PlayerClass>().playerSpeed = initSp * (1+coefSp);
        initAngSp = player.GetComponent<PlayerClass>().playerAngularSpeed = initAngSp * (1+coefAngSp);
        crit = initCrit * (1+coefCrit);

        manager.GetComponent<Pause>().StatsRenderer();

        coefAtt = 0;
        coefAttSp = 0;
        coefDef = 0;
        coefSp = 0;
        coefAngSp = 0;
        coefCrit = 0;

    }
}