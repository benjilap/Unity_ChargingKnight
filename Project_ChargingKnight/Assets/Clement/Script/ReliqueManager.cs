using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReliqueManager : MonoBehaviour {

    public int nombreSlots;
    List<GameObject> listRelics = new List<GameObject>();
    int relicToSpawn;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnRelic(Transform trans)
    {
        relicToSpawn = Random.Range(0, listRelics.Count);
    }

}
