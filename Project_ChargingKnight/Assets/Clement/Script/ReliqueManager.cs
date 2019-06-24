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



    // Use this for initialization
    void Start()
    {
        manager = GameObject.Find("MANAGER");
        relicPrefab = Resources.Load("Relics/Relic");
        player = GameObject.Find("Player1");
    }

    // Update is called once per frame
    void Update()
    {


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
        }
    }

    void SpawnRelic(Vector3 posi)
    {

        if (posi != null)
            lastCreated = Instantiate(relicPrefab, posi - new Vector3(0,0,2), new Quaternion(0, 0, 0, 0)) as GameObject;
        else lastCreated = Instantiate(relicPrefab, player.transform.position, new Quaternion(0, 0, 0, 0)) as GameObject;       

    }

    public void StatModifier(GameObject modifier)
    {

    }
}