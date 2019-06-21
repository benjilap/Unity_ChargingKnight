using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM_CanvasScript : MonoBehaviour {

    [HideInInspector]
    public List<GameObject> listOfPlayers = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> listOfPlayersCanvas = new List<GameObject>();

    Object playerUI;

    private void Start()
    {
        playerUI = Resources.Load("Canvas/UIPlayer");
    }

    private void Update()
    {
        CheckPlayerIsReady();
    }

    void CheckPlayerIsReady()
    {
        if (listOfPlayers.Count == GameManager.playersNmbrs)
        {
            if( listOfPlayers[listOfPlayers.Count-1].GetComponent<PlayerController>().controllerNum != 0)
            {
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
}
