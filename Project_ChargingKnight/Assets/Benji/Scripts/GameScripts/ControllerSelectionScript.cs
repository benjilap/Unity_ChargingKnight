using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerSelectionScript : MonoBehaviour {



    GM_CanvasScript GameCanvasScript;
    List<ControllerSelection> listOfContSelect = new List<ControllerSelection>();

    Object controllerSelectCanvas;

    int playerToCheck = 1;
    int[] selectableController;

    void Start()
    {
        Input.GetJoystickNames().Initialize();
        GameCanvasScript = this.GetComponent<GM_CanvasScript>();
        controllerSelectCanvas = Resources.Load("Canvas/ControllerSelection");
        CreateControllerSelection();
        SetSelectableController();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameCanvasScript.listOfPlayers[0].GetComponent<PlayerController>().controllerNum ==0)
        {
            CreateControllerSelection();
        }
        CheckController();
        //foreach(GameObject player in listOfPlayers)
        //{
        //    Debug.Log(player.GetComponent<PlayerClass>().playerNum);
        //}
    }

    public void CreateControllerSelection()
    {
        if (listOfContSelect.Count < GameManager.playersNmbrs)
        {
            for (int i = 0; i < GameCanvasScript.listOfPlayers.Count; i++)
            {

                ControllerSelection newContSelect = new ControllerSelection();
                newContSelect.SetSelectCont(GameCanvasScript.listOfPlayers[i].GetComponent<PlayerClass>().playerNum, 0, GameCanvasScript.CreateCanvas(controllerSelectCanvas,i));
                listOfContSelect.Add(newContSelect);

            }
        }
    }

    void CheckController()
    {

        if (playerToCheck != 0)
        {
            if (playerToCheck == listOfContSelect[playerToCheck - 1].playerNum)
            {
                for (int i = 0; i < selectableController.Length; i++)
                {
                    if (selectableController[i] != 0)
                    {
                        if (Input.GetButton("J" + selectableController[i] + "Start"))
                        {
                            listOfContSelect[playerToCheck - 1].controllerNum = selectableController[i];
                            selectableController[i] = 0;
                            AssignPlCtlr(listOfContSelect[playerToCheck - 1], playerToCheck);
                            if (playerToCheck < GameCanvasScript.listOfPlayers.Count)
                            {
                                playerToCheck++;
                            }
                            else
                            {
                                playerToCheck = 0;
                            }
                        }
                    }
                }
            }
        }
        else if (listOfContSelect.Count != 0)
        {
            listOfContSelect = new List<ControllerSelection>();
        }
    }

    void SetSelectableController()
    {
        selectableController = new int[Input.GetJoystickNames().Length];

        for (int i = 0; i < Input.GetJoystickNames().Length; i++)
        {
            Debug.Log(Input.GetJoystickNames()[i]);
            selectableController.SetValue(i + 1, i);
        }

    }

    void AssignPlCtlr(ControllerSelection newCtlr, int playerNum)
    {
        foreach (GameObject player in GameCanvasScript.listOfPlayers)
        {
            if (player.GetComponent<PlayerClass>().playerNum == playerNum)
            {
                player.GetComponent<PlayerController>().controllerNum = newCtlr.controllerNum;
                Destroy(newCtlr.selectionCanvas);
            }
        }
    }
}

public class ControllerSelection
{
    public int playerNum;
    public int controllerNum;
    public GameObject selectionCanvas;

    public void SetSelectCont(int plNum, int ctlrNum, GameObject sltCanvas)
    {
        playerNum = plNum;
        controllerNum = ctlrNum;
        selectionCanvas = sltCanvas;
    }
}
