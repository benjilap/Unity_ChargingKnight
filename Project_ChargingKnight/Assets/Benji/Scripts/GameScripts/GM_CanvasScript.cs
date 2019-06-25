using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_CanvasScript : MonoBehaviour {

    [HideInInspector]
    public List<GameObject> listOfPlayers = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> listOfPlayersCanvas = new List<GameObject>();
    //[HideInInspector]
    public bool playersReady;

    Object playerUI;
    Object gameOverFlame;


    private void Start()
    {
        playerUI = Resources.Load("Canvas/UIPlayer");
        gameOverFlame = Resources.Load("Canvas/GameOver");

    }

    private void Update()
    {
        listOfPlayers.Remove(null);
        CheckPlayerIsReady();
        CheckPlayersDeath();
    }

    void CheckPlayerIsReady()
    {
        if (listOfPlayers.Count == GameManager.playersNmbrs)
        {
            bool check = true;
            foreach(GameObject player in listOfPlayers)
            {
                if(player.GetComponent<PlayerController>().controllerNum == 0)
                {
                    check = false;
                }
            }
            if(check)
            {
                playersReady = true;
                CreatePlayerUI();
                
            }
        }
    }

    public GameObject CreateCanvas(Object objectToAddInCanvas, int CanvasPos)
    {
        float[] ImageAnchorLimit = { 0, 1 };
        ImageAnchorLimit[1] = ImageAnchorLimit[1] / listOfPlayers.Count;
        ImageAnchorLimit = new float[] { ImageAnchorLimit[1] * CanvasPos, ImageAnchorLimit[1] * (CanvasPos + 1) };
        GameObject myNewObject = Instantiate(objectToAddInCanvas, this.transform.position, Quaternion.identity) as GameObject;
        myNewObject.name = objectToAddInCanvas.name;
        myNewObject.transform.SetParent(this.transform);
        myNewObject.GetComponent<RectTransform>().anchorMin = new Vector2(ImageAnchorLimit[0], 0);
        myNewObject.GetComponent<RectTransform>().anchorMax = new Vector2(ImageAnchorLimit[1], 1);
        myNewObject.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        myNewObject.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        return myNewObject;
    }

    void CreatePlayerUI()
    {
        if (listOfPlayersCanvas.Count < GameManager.playersNmbrs)
        {
            for (int i = 0; i < listOfPlayers.Count; i++)
            {

                listOfPlayersCanvas.Add(CreateCanvas(playerUI,i));
                listOfPlayersCanvas[i].GetComponent<PlayerUIScript>().myPlayer = listOfPlayers[i].GetComponent<PlayerClass>();
            }
        }
    }

    void CheckPlayersDeath()
    {
        if (FindObjectOfType<GameOverScript>() == null)
        {
            if (listOfPlayers.Count == 0)
            {
                CreateGameOver();
            }
        }
    }

    void CreateGameOver()
    {
        GameObject GameOverScreen = Instantiate(gameOverFlame, Vector3.zero, Quaternion.identity) as GameObject;
        GameOverScreen.transform.SetParent(this.transform);
        GameOverScreen.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        GameOverScreen.GetComponent<RectTransform>().offsetMax = Vector2.zero;
    }
}
