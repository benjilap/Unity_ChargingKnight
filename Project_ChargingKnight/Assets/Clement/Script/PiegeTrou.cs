using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PiègeTrou : MonoBehaviour {

    GameObject player;
    public float degats;
    Vector3 velocity;
    Vector3 vecWithPl;
    float playerX;
    float playerZ;
    float playerY;
    float boundMinX;
    float boundMaxX;
    float boundMinZ;
    float boundMaxZ;

	// Use this for initialization
	void Start () {

        player = GameObject.Find("Player");
        float boundMinX = gameObject.GetComponent<Collider>().bounds.min.x;
        float boundMaxX = gameObject.GetComponent<Collider>().bounds.max.x;
        float boundMinZ = gameObject.GetComponent<Collider>().bounds.min.z;
        float boundMaxZ = gameObject.GetComponent<Collider>().bounds.max.z;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (player.CompareTag("Player") == true)
        {

            velocity = player.GetComponent<Rigidbody>().velocity;
            playerZ = player.transform.position.z;
            playerY = player.transform.position.y;
            playerX  = player.transform.position.x;
            player.GetComponent<PlayerLifeManager>().life = player.GetComponent<PlayerLifeManager>().life - degats;
            
            if(velocity.x + Vector3.zero.x < 0)
            {
                player.transform.position.Set(boundMaxX + 1, playerY, playerZ);
                //vecWithPl = new Vector3(-1, playerY, playerZ);
                player.transform.LookAt(player.transform.position + Vector3.forward);
            }
        }
    }
}
