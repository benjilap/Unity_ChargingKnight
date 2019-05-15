using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour {

    [SerializeField]
    float spawnHeight;
    [SerializeField]
    float spawnOffset = 2;
    Vector3 startPos;

    Object playerPrefab;
    Object camPrefab;
    Object GM_Canvas;

    List<GameObject> actualsPlayer = new List<GameObject>();


    void Start () {
        playerPrefab = Resources.Load("Player/Player");
        camPrefab = Resources.Load("Player/GameCamera");
        GM_Canvas = Resources.Load("Canvas/GMCanvas");

        CheckPlayer();
        CheckCam();
    }


    void Update()
    {
        CheckPlayer();
        CheckCam();
        CheckPlayerList();
        CheckGM_Canvas();

    }

    void CheckPlayer()
    {
        if (GameManager.playersNmbrs == 2)
        {

            GameObject[] currentPlayers = GameObject.FindGameObjectsWithTag("Player");
            if (currentPlayers.Length == 0)
            {

                for(int i=1; i <= GameManager.playersNmbrs; i++)
                {
                    GameObject myNewPlayer = Instantiate(playerPrefab, SetStartPos(i, spawnOffset), Quaternion.identity) as GameObject;
                    if (myNewPlayer.GetComponent<PlayerClass>() != null)
                    {
                        myNewPlayer.GetComponent<PlayerClass>().playerNum = i;
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
                GameObject myNewPlayer = Instantiate(playerPrefab, SetStartPos(0,0), Quaternion.identity) as GameObject;
                if (myNewPlayer.GetComponent<PlayerClass>() != null)
                {
                    myNewPlayer.GetComponent<PlayerClass>().playerNum = GameManager.playersNmbrs;
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
            GameObject myNewCam = Instantiate(camPrefab, SetStartPos(0,0), Quaternion.identity) as GameObject;
            myNewCam.name = camPrefab.name;
        }
    }

    void CheckGM_Canvas()
    {
        if (GameObject.FindObjectOfType<GM_CanvasScript>() == null)
        {
            GameObject myNewGMCanvas = Instantiate(GM_Canvas, Vector3.zero, Quaternion.identity) as GameObject;
            myNewGMCanvas.GetComponent<GM_CanvasScript>().listOfPlayers = actualsPlayer;
            myNewGMCanvas.name = GM_Canvas.name;
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

    Vector3 SetStartPos(int playerNum, float playerOffset)
    {
        if (playerNum == 2)
        {
            return startPos = new Vector3(playerOffset, spawnHeight, 0);

        }
        else if (playerNum == 1)
        {
            return startPos = new Vector3(-playerOffset, spawnHeight, 0);

        }
        else
        {
            return startPos = new Vector3(0, spawnHeight, 0);

        }
    }

}
