using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour {

    [SerializeField]
    float spawnHeight;
    Vector3 startPos;

    Object playerPrefab;
    Object camPrefab;

    List<GameObject> actualsPlayer = new List<GameObject>();


    void Start () {
        playerPrefab = Resources.Load("Player/Player");
        camPrefab = Resources.Load("Player/GameCamera");
        CheckPlayer();
        CheckCam();
    }


    void Update()
    {
        CheckPlayer();
        CheckCam();
        CheckPlayerList();

    }

    void CheckPlayer()
    {
        if (GameManager.playersNmbrs == 2)
        {

            GameObject[] currentPlayers = GameObject.FindGameObjectsWithTag("Player");
            if (currentPlayers.Length == 0)
            {
            Debug.Log(currentPlayers.Length);
                for(int i=1; i <= GameManager.playersNmbrs; i++)
                {
                    GameObject myNewPlayer = Instantiate(playerPrefab, SetStartPos(i), Quaternion.identity) as GameObject;
                    if (myNewPlayer.GetComponent<PlayerController>() != null)
                    {
                        myNewPlayer.GetComponent<PlayerController>().playerNum = i;
                        myNewPlayer.name = "Player" + i.ToString();

                    }
                    actualsPlayer.Add(myNewPlayer);
                }
            }
        }
        else if(GameManager.playersNmbrs == 1)
        {
            if (GameObject.FindGameObjectWithTag("Player") == null)
            {
                GameObject myNewPlayer = Instantiate(playerPrefab, SetStartPos(0), Quaternion.identity) as GameObject;
                if (myNewPlayer.GetComponent<PlayerController>() != null)
                {
                    myNewPlayer.GetComponent<PlayerController>().playerNum = GameManager.playersNmbrs;
                    myNewPlayer.name = "Player" + GameManager.playersNmbrs.ToString();
                }
                actualsPlayer.Add(myNewPlayer);
            }
        }
    }

    void CheckCam()
    {
        if (GameObject.FindObjectOfType<CameraScript>() == null)
        {
            GameObject myNewCam = Instantiate(camPrefab, SetStartPos(0), Quaternion.identity) as GameObject;
        }
    }

    void CheckPlayerList()
    {
        for(int i = 0; i < actualsPlayer.Count; i++)
        {
            if(actualsPlayer[i] == null)
            {
                actualsPlayer.Remove(null);
            }
        }
    }

    Vector3 SetStartPos(int playerNum)
    {
        if (playerNum == 2)
        {
            return startPos = new Vector3(-1, spawnHeight, 0);

        }
        else if (playerNum == 1)
        {
            return startPos = new Vector3(1, spawnHeight, 0);

        }
        else
        {
            return startPos = new Vector3(0, spawnHeight, 0);

        }
    }

}
