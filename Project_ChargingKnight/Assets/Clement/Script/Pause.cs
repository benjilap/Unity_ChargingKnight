using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour {

    [SerializeField]
    int forTime;
    bool gonePaused;
    bool isPaused;
    bool isInReliques;
    [SerializeField]
    float pauseFadeDuration = 0.3f;
    GameObject menuPause;
    GameObject menuQuitter;
    GameObject menuReliques;
    GameObject menuStats;
    List<Transform> listMenuPause = new List<Transform>();
    List<Transform> listMenuQuitter = new List<Transform>();
    List<Transform> listMenuReliques = new List<Transform>();
    GameObject curseurPause;
    GameObject curseurQuitter;
    GameObject player;
    PlayerController playerOneController;
    PlayerController playerTwoController;
    int currentCursor;
    int currentSlot;
    bool canMoveCursor;
    int nombreSlots;
    GameObject manager;
    GameObject slot;
    GameObject curseurReliques;
    GameObject lastSlot;



    // Use this for initialization
    void Start () {

        canMoveCursor = true;
        gonePaused = false;
        isPaused = false;
        isInReliques = false;
        manager = GameObject.Find("RelicSpawner");
        nombreSlots = manager.GetComponent<ReliqueManager>().nombreSlots;
        menuPause = GameObject.Find("MenuPause");
        menuQuitter = GameObject.Find("MenuQuitter");
        menuReliques = GameObject.Find("MenuReliques");
        menuStats = GameObject.Find("MenuStats");
        curseurReliques = menuReliques.transform.GetChild(1).gameObject;
        curseurReliques.transform.SetAsFirstSibling();
        slot = menuReliques.transform.GetChild(1).gameObject;


        Time.timeScale = 0f;

        for(int i = 0; i < nombreSlots - 1; i++)
        {
            lastSlot = Instantiate(slot, menuReliques.transform);
            lastSlot.name = "Slot" + (i + 1);
        }

        for(int i = 0; i < menuReliques.transform.childCount; i++)
        {
            listMenuReliques.Add(menuReliques.transform.GetChild(i));
        }
        listMenuReliques.RemoveAt(0);
        listMenuReliques.RemoveAt(1);
        currentSlot = 0;
        curseurReliques.SetActive(false);
        menuReliques.SetActive(false);

        for (int i = 0; i < menuPause.transform.childCount; i++ )
        {            
            listMenuPause.Add(menuPause.transform.GetChild(i));
        }
        listMenuPause.RemoveAt(0);
        curseurPause = listMenuPause[0].gameObject;

        for (int i = 0; i < menuQuitter.transform.childCount; i++)
        {
            listMenuQuitter.Add(menuQuitter.transform.GetChild(i));
        }
        listMenuQuitter.RemoveAt(0);
        listMenuQuitter.RemoveAt(1);
        curseurQuitter = listMenuQuitter[0].gameObject;
        curseurQuitter.transform.localPosition = listMenuQuitter[1].transform.localPosition;
        menuQuitter.SetActive(false);

        curseurPause = listMenuPause[0].gameObject;
        
       
       
        currentCursor = 1;
        menuPause.SetActive(false);
        menuStats.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player1") && GameObject.Find("Player2") != null)
        {
            playerOneController = GameObject.Find("Player1").GetComponent<PlayerController>();
            playerTwoController = GameObject.Find("Player2").GetComponent<PlayerController>();
        }
        else
        {
            playerOneController = GameObject.Find("Player1").GetComponent<PlayerController>();
            playerTwoController = null;
        }

        ////Debug.Log(currentSlot);
        //////Debug.Log(listMenuReliques.Count);
        if (playerOneController && playerTwoController != null)
        {
            {
                if (playerOneController.Start() )
                {
                    pause();
                    menuStats.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = ("Dégats : " + 0); //Inserer valeur attaque joueur
                    menuStats.transform.GetChild(2).GetChild(0).gameObject.GetComponent<Text>().text = ("Vitesse d'attaque : " + 0); //Inserer valeur vitesse attaque joueur
                    menuStats.transform.GetChild(3).GetChild(0).gameObject.GetComponent<Text>().text = ("Defense : " + 0); //Inserer valeur defense joueur
                    menuStats.transform.GetChild(4).GetChild(0).gameObject.GetComponent<Text>().text = ("Vitesse : " + (GameObject.Find("Player1").GetComponent<PlayerClass>().playerSpeed));
                    menuStats.transform.GetChild(5).GetChild(0).gameObject.GetComponent<Text>().text = ("Vitesse rotation : " + (GameObject.Find("Player1").GetComponent<PlayerClass>().playerAngularSpeed));
                    menuStats.transform.GetChild(6).GetChild(0).gameObject.GetComponent<Text>().text = ("Taux critique : " + 0); //Inserer valeur % critique joueur
                }
                else if(playerTwoController.Start())
                {
                    pause();
                    menuStats.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = ("Dégats : " + 0); //Inserer valeur attaque joueur
                    menuStats.transform.GetChild(2).GetChild(0).gameObject.GetComponent<Text>().text = ("Vitesse d'attaque : " + 0); //Inserer valeur vitesse attaque joueur
                    menuStats.transform.GetChild(3).GetChild(0).gameObject.GetComponent<Text>().text = ("Defense : " + 0); //Inserer valeur defense joueur
                    menuStats.transform.GetChild(4).GetChild(0).gameObject.GetComponent<Text>().text = ("Vitesse : " + (GameObject.Find("Player2").GetComponent<PlayerClass>().playerSpeed));
                    menuStats.transform.GetChild(5).GetChild(0).gameObject.GetComponent<Text>().text = ("Vitesse rotation : " + (GameObject.Find("Player2").GetComponent<PlayerClass>().playerAngularSpeed));
                    menuStats.transform.GetChild(6).GetChild(0).gameObject.GetComponent<Text>().text = ("Taux critique : " + 0); //Inserer valeur % critique joueur
                }

                if (isInReliques == false && menuPause.active == true && isPaused && ((playerOneController.HorizontalAxisRaw() < 0) || (playerTwoController.HorizontalAxisRaw() < 0)))
                {
                    isInReliques = true;
                    curseurReliques.SetActive(true);
                    curseurPause.SetActive(false);
                }

                if (isInReliques && isPaused)
                {
                    if (playerOneController.HorizontalAxisRaw() != 0 || playerTwoController.HorizontalAxisRaw() != 0)
                    {
                        if ((playerOneController.HorizontalAxisRaw() < 0 || playerTwoController.HorizontalAxisRaw() < 0) && canMoveCursor)
                        {
                            if ((currentSlot == 0 || currentSlot % 3 == 0) && canMoveCursor)
                            {
                                if (currentSlot + 2 < listMenuReliques.Count)
                                {
                                    ////Debug.Log("yolo");
                                    currentSlot = currentSlot + 2;
                                    curseurReliques.transform.position = listMenuReliques[currentSlot].position;
                                    canMoveCursor = false;
                                    StartCoroutine(MoveCursor());
                                }
                                else if (currentSlot + 1 < listMenuReliques.Count)
                                {
                                    ////Debug.Log("ui");
                                    currentSlot = currentSlot + 1;
                                    curseurReliques.transform.position = listMenuReliques[currentSlot].position;
                                    canMoveCursor = false;
                                    StartCoroutine(MoveCursor());
                                }
                                else { }
                                ////Debug.Log("jaaj");
                            }
                            else
                            {
                                ////Debug.Log("merde");
                                currentSlot--;
                                curseurReliques.transform.position = listMenuReliques[currentSlot].position;
                                canMoveCursor = false;
                                StartCoroutine(MoveCursor());
                            }
                        }
                        else if ((playerOneController.HorizontalAxisRaw() > 0 || playerTwoController.HorizontalAxisRaw() > 0) && canMoveCursor)
                        {
                            if ((currentSlot + 1) % 3 == 0 && canMoveCursor)
                            {
                                currentSlot = currentSlot - 2;
                                curseurReliques.transform.position = listMenuReliques[currentSlot].position;
                                canMoveCursor = false;
                                StartCoroutine(MoveCursor());
                            }
                            else if (currentSlot + 1 < listMenuReliques.Count)
                            {
                                currentSlot++;
                                curseurReliques.transform.position = listMenuReliques[currentSlot].position;
                                canMoveCursor = false;
                                StartCoroutine(MoveCursor());
                            }
                            else if (currentSlot % 3 == 1 && currentSlot + 1 >= listMenuReliques.Count)
                            {
                                currentSlot = currentSlot - 1;
                                curseurReliques.transform.position = listMenuReliques[currentSlot].position;
                                canMoveCursor = false;
                                StartCoroutine(MoveCursor());
                            }

                        }
                    }
                    if (playerOneController.VerticalAxisRaw() != 0 || playerTwoController.VerticalAxisRaw() != 0)
                    {
                        if ((playerOneController.VerticalAxisRaw() < 0 || playerTwoController.VerticalAxisRaw() < 0) && canMoveCursor)
                        {
                            //Debug.Log(currentSlot);
                            if (currentSlot + 3 < listMenuReliques.Count)
                            {
                                //Debug.Log("pute");
                                currentSlot = currentSlot + 3;
                                curseurReliques.transform.position = listMenuReliques[currentSlot].position;
                                canMoveCursor = false;
                                StartCoroutine(MoveCursor());
                            }
                            else
                            {
                                //Debug.Log("reste" + currentSlot % 3);
                                switch (currentSlot % 3)
                                {
                                    case 0:
                                        currentSlot = 0;
                                        curseurReliques.transform.position = listMenuReliques[currentSlot].position;
                                        canMoveCursor = false;
                                        StartCoroutine(MoveCursor());
                                        break;
                                    case 1:
                                        currentSlot = 1;
                                        curseurReliques.transform.position = listMenuReliques[currentSlot].position;
                                        canMoveCursor = false;
                                        StartCoroutine(MoveCursor());
                                        break;
                                    case 2:
                                        currentSlot = 2;
                                        curseurReliques.transform.position = listMenuReliques[currentSlot].position;
                                        canMoveCursor = false;
                                        StartCoroutine(MoveCursor());
                                        break;
                                }
                            }

                        }
                        else if ((playerOneController.VerticalAxisRaw() > 0 || playerOneController.VerticalAxisRaw() > 0) && canMoveCursor)
                        {
                            if (currentSlot - 3 >= 0)
                            {
                                currentSlot = currentSlot - 3;
                                curseurReliques.transform.position = listMenuReliques[currentSlot].position;
                                canMoveCursor = false;
                                StartCoroutine(MoveCursor());
                            }
                            else
                            {
                                /*for (int i = 0; i < listMenuReliques.Count; i++)
                                {
                                    //Debug.Log(i + "=" + listMenuReliques[i]);
                                }*/
                                switch (currentSlot % 3)
                                {
                                    case 0:
                                        for (int i = 1; i < 4; i++)
                                        {
                                            if ((listMenuReliques.Count - i) % 3 == 0)
                                            {
                                                currentSlot = listMenuReliques.Count - i;
                                                //Debug.Log(currentSlot);
                                                //Debug.Log(listMenuReliques[3].position);
                                                //Debug.Log(listMenuReliques[9].position);
                                                curseurReliques.transform.position = listMenuReliques[currentSlot].position;
                                                canMoveCursor = false;
                                                StartCoroutine(MoveCursor());
                                            }

                                        }
                                        break;
                                    case 1:
                                        for (int i = 1; i < 4; i++)
                                        {
                                            if ((listMenuReliques.Count - i) % 3 == 1)
                                            {
                                                currentSlot = listMenuReliques.Count - i;
                                                curseurReliques.transform.position = listMenuReliques[currentSlot].position;
                                                canMoveCursor = false;
                                                StartCoroutine(MoveCursor());
                                            }
                                        }
                                        break;
                                    case 2:
                                        for (int i = 1; i < 4; i++)
                                        {
                                            if ((listMenuReliques.Count - i) % 3 == 2)
                                            {
                                                currentSlot = listMenuReliques.Count - i;
                                                curseurReliques.transform.position = listMenuReliques[currentSlot].position;
                                                canMoveCursor = false;
                                                StartCoroutine(MoveCursor());
                                            }
                                        }
                                        break;
                                }
                            }

                        }
                    }
                    if (playerOneController.ButtonB() || playerTwoController.ButtonB())
                    {
                        currentCursor = 1;
                        currentSlot = 0;
                        curseurReliques.SetActive(false);
                        curseurPause.SetActive(true);
                        isInReliques = false;
                    }
                }

                if (isInReliques == false && menuPause.active == true && isPaused && (playerOneController.VerticalAxisRaw() != 0 || playerTwoController.VerticalAxisRaw() !=0))
                {
                    if ((playerOneController.VerticalAxisRaw() < 0 || playerTwoController.VerticalAxisRaw() < 0) && currentCursor < (listMenuPause.Count - 1) && canMoveCursor)
                    {
                        currentCursor++;
                        curseurPause.transform.localPosition = listMenuPause[currentCursor].localPosition;
                        canMoveCursor = false;
                        StartCoroutine(MoveCursor());
                    }
                    else if ((playerOneController.VerticalAxisRaw() > 0 || playerTwoController.VerticalAxisRaw() > 0) && currentCursor > 1 && canMoveCursor)
                    {
                        currentCursor--;
                        curseurPause.transform.localPosition = listMenuPause[currentCursor].localPosition;
                        canMoveCursor = false;
                        StartCoroutine(MoveCursor());
                    }
                    else if ((playerOneController.VerticalAxisRaw() > 0 || playerTwoController.VerticalAxisRaw() > 0) && currentCursor == 1 && canMoveCursor)
                    {
                        currentCursor = 4;
                        curseurPause.transform.localPosition = listMenuPause[currentCursor].localPosition;
                        canMoveCursor = false;
                        StartCoroutine(MoveCursor());
                    }
                    if ((playerOneController.VerticalAxisRaw() < 0 || playerTwoController.VerticalAxisRaw() < 0) && currentCursor == (listMenuPause.Count - 1) && canMoveCursor)
                    {
                        currentCursor = 1;
                        curseurPause.transform.localPosition = listMenuPause[currentCursor].localPosition;
                        canMoveCursor = false;
                        StartCoroutine(MoveCursor());
                    }
                }
                else if (isInReliques == false && menuPause.active == true && isPaused && (playerOneController.HorizontalAxisRaw() != 0 ||playerTwoController.HorizontalAxisRaw() !=0))
                {
                    if ((playerOneController.HorizontalAxisRaw() < 0 || playerTwoController.HorizontalAxisRaw() < 0) && canMoveCursor)
                    {
                        isInReliques = true;
                        curseurPause.SetActive(false);
                        curseurReliques.SetActive(true);
                        currentSlot = 0;
                        curseurReliques.transform.position = listMenuReliques[currentSlot].position;
                        canMoveCursor = false;
                        StartCoroutine(MoveCursor());
                    }
                    else if ((playerOneController.HorizontalAxisRaw() > 0 || playerOneController.HorizontalAxisRaw() > 0) && canMoveCursor)
                    {
                        isInReliques = true;
                        curseurPause.SetActive(false);
                        curseurReliques.SetActive(true);
                        currentSlot = 0;
                        curseurReliques.transform.position = listMenuReliques[currentSlot].position;
                        canMoveCursor = false;
                        StartCoroutine(MoveCursor());
                    }
                }
                if ((playerOneController.ButtonA() || playerTwoController.ButtonA()) && isInReliques == false && isPaused && menuPause.active == true)
                {
                    switch (currentCursor)
                    {
                        case 1:
                            pause();
                            currentCursor = 1;
                            break;
                        case 2:
                            currentCursor = 1;
                            break;
                        case 3:
                            currentCursor = 1;
                            break;
                        case 4:
                            menuQuitter.SetActive(true);
                            menuPause.SetActive(false);
                            menuReliques.SetActive(false);
                            menuStats.SetActive(false);
                            currentCursor = 1;
                            break;
                    }
                }

                if (menuQuitter.active == true && isPaused && (playerOneController.HorizontalAxisRaw() != 0 || playerTwoController.HorizontalAxisRaw() != 0))
                {
                    if ((playerOneController.HorizontalAxisRaw() != 0 || playerTwoController.HorizontalAxisRaw() != 0) && currentCursor == 2 && canMoveCursor)
                    {
                        currentCursor--;
                        curseurQuitter.transform.localPosition = listMenuQuitter[currentCursor].localPosition;
                        canMoveCursor = false;
                        StartCoroutine(MoveCursor());
                    }
                    if (playerOneController.HorizontalAxisRaw() != 0 || playerTwoController.HorizontalAxisRaw() != 0 && currentCursor == 1 && canMoveCursor)
                    {
                        currentCursor++;
                        curseurQuitter.transform.localPosition = listMenuQuitter[currentCursor].localPosition;
                        canMoveCursor = false;
                        StartCoroutine(MoveCursor());
                    }

                }
                if ((playerOneController.ButtonA() || playerTwoController.ButtonA()) && isPaused && menuQuitter.active == true)
                {
                    switch (currentCursor)
                    {
                        case 1:
                            Application.Quit();
                            break;
                        case 2:
                            menuQuitter.SetActive(false);
                            menuPause.SetActive(true);
                            menuReliques.SetActive(true);
                            menuStats.SetActive(true);
                            currentCursor = 5;
                            break;
                        default:
                            break;

                    }
                }
                else if ((playerOneController.ButtonB() || playerTwoController.ButtonB()) && isPaused && menuQuitter.active == true)
                {
                    menuQuitter.SetActive(false);
                    menuPause.SetActive(true);
                    menuReliques.SetActive(true);
                    menuStats.SetActive(true);
                    currentCursor = 5;

                }
            }
        }
        ///////////////////
        ///////////////////
        else
        {
            if (playerOneController.Start())
            {
                pause();
                menuStats.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = ("Dégats : " + 0); //Inserer valeur attaque joueur
                menuStats.transform.GetChild(2).GetChild(0).gameObject.GetComponent<Text>().text = ("Vitesse d'attaque : " + 0); //Inserer valeur vitesse attaque joueur
                menuStats.transform.GetChild(3).GetChild(0).gameObject.GetComponent<Text>().text = ("Defense : " + 0); //Inserer valeur defense joueur
                menuStats.transform.GetChild(4).GetChild(0).gameObject.GetComponent<Text>().text = ("Vitesse : " + (GameObject.Find("Player1").GetComponent<PlayerClass>().playerSpeed));
                menuStats.transform.GetChild(5).GetChild(0).gameObject.GetComponent<Text>().text = ("Vitesse rotation : " + (GameObject.Find("Player1").GetComponent<PlayerClass>().playerAngularSpeed));
                menuStats.transform.GetChild(6).GetChild(0).gameObject.GetComponent<Text>().text = ("Taux critique : " + 0); //Inserer valeur % critique joueur
            }

            if (isInReliques == false && menuPause.active == true && isPaused && (playerOneController.HorizontalAxisRaw() < 0))
            {
                isInReliques = true;
                curseurReliques.SetActive(true);
                curseurPause.SetActive(false);
            }

            if(isInReliques && isPaused)
            {
                if(playerOneController.HorizontalAxisRaw() != 0)
                {
                    if(playerOneController.HorizontalAxisRaw() < 0 && canMoveCursor )
                    {
                        if ((currentSlot == 0 ||currentSlot % 3 == 0) && canMoveCursor )
                        {
                            if (currentSlot + 2 < listMenuReliques.Count)
                            {
                                ////Debug.Log("yolo");
                                currentSlot = currentSlot + 2;
                                curseurReliques.transform.position = listMenuReliques[currentSlot].position;
                                canMoveCursor = false;
                                StartCoroutine(MoveCursor());
                            }
                            else if (currentSlot + 1 < listMenuReliques.Count)
                            {
                                ////Debug.Log("ui");
                                currentSlot = currentSlot + 1;
                                curseurReliques.transform.position = listMenuReliques[currentSlot].position;
                                canMoveCursor = false;
                                StartCoroutine(MoveCursor());
                            }
                            else { }
                                ////Debug.Log("jaaj");
                        }
                        else
                        {
                            ////Debug.Log("merde");
                            currentSlot--;
                            curseurReliques.transform.position = listMenuReliques[currentSlot].position;
                            canMoveCursor = false;
                            StartCoroutine(MoveCursor());
                        }
                    }
                    else if(playerOneController.HorizontalAxisRaw() > 0 && canMoveCursor )
                    {
                        if ((currentSlot + 1) % 3 == 0 && canMoveCursor)
                        {
                            currentSlot = currentSlot - 2;
                            curseurReliques.transform.position = listMenuReliques[currentSlot].position;
                            canMoveCursor = false;
                            StartCoroutine(MoveCursor());
                        }
                        else if (currentSlot + 1 < listMenuReliques.Count)
                        {
                            currentSlot++;
                            curseurReliques.transform.position = listMenuReliques[currentSlot].position;
                            canMoveCursor = false;
                            StartCoroutine(MoveCursor());
                        }
                        else if (currentSlot % 3 == 1 && currentSlot + 1 >= listMenuReliques.Count)
                        {
                            currentSlot = currentSlot - 1;
                            curseurReliques.transform.position = listMenuReliques[currentSlot].position;
                            canMoveCursor = false;
                            StartCoroutine(MoveCursor());
                        }
                                                
                    }
                }
                if (playerOneController.VerticalAxisRaw() != 0)
                {
                    if (playerOneController.VerticalAxisRaw() < 0 && canMoveCursor)
                    {
                        //Debug.Log(currentSlot);
                        if(currentSlot + 3<listMenuReliques.Count)
                        {
                            //Debug.Log("pute");
                            currentSlot = currentSlot + 3;
                            curseurReliques.transform.position = listMenuReliques[currentSlot].position;
                            canMoveCursor = false;
                            StartCoroutine(MoveCursor());
                        }
                        else 
                        {
                            //Debug.Log("reste" + currentSlot % 3);
                            switch( currentSlot % 3)
                            { 
                                case 0 :
                                    currentSlot = 0;
                                    curseurReliques.transform.position = listMenuReliques[currentSlot].position;
                                    canMoveCursor = false;
                                    StartCoroutine(MoveCursor());
                                    break;
                                case 1:
                                    currentSlot = 1;
                                    curseurReliques.transform.position = listMenuReliques[currentSlot].position;
                                    canMoveCursor = false;
                                    StartCoroutine(MoveCursor());
                                    break;
                                case 2:
                                    currentSlot = 2;
                                    curseurReliques.transform.position = listMenuReliques[currentSlot].position;
                                    canMoveCursor = false;
                                    StartCoroutine(MoveCursor());
                                    break;
                            }
                        }
                        
                    }
                    else if (playerOneController.VerticalAxisRaw() > 0 && canMoveCursor)
                    {
                        if (currentSlot - 3 >= 0 )
                        {
                            currentSlot = currentSlot - 3;
                            curseurReliques.transform.position = listMenuReliques[currentSlot].position;
                            canMoveCursor = false;
                            StartCoroutine(MoveCursor());
                        }
                        else
                        {
                            /*for (int i = 0; i < listMenuReliques.Count; i++)
                            {
                                //Debug.Log(i + "=" + listMenuReliques[i]);
                            }*/
                            switch (currentSlot % 3)
                            {
                                case 0: for(int i = 1; i<4; i++)
                                    {
                                        if ((listMenuReliques.Count - i) % 3 == 0)
                                        {
                                            currentSlot = listMenuReliques.Count - i;
                                            //Debug.Log(currentSlot);
                                            //Debug.Log(listMenuReliques[3].position);
                                            //Debug.Log(listMenuReliques[9].position);
                                            curseurReliques.transform.position = listMenuReliques[currentSlot].position;
                                            canMoveCursor = false;
                                            StartCoroutine(MoveCursor());                                         
                                        }
                                       
                                    }
                                    break;
                                case 1:
                                    for (int i = 1; i < 4; i++)
                                    {
                                        if ((listMenuReliques.Count - i) % 3 == 1)
                                        {
                                            currentSlot = listMenuReliques.Count - i;
                                            curseurReliques.transform.position = listMenuReliques[currentSlot].position;
                                            canMoveCursor = false;
                                            StartCoroutine(MoveCursor());
                                        }
                                    }
                                    break;
                                case 2:
                                    for (int i = 1; i < 4; i++)
                                    {
                                        if ((listMenuReliques.Count - i) % 3 == 2)
                                        {
                                            currentSlot = listMenuReliques.Count - i;
                                            curseurReliques.transform.position = listMenuReliques[currentSlot].position;
                                            canMoveCursor = false;
                                            StartCoroutine(MoveCursor());
                                        }
                                    }
                                    break;
                            }                            
                        }

                    }
                }
                if (playerOneController.ButtonB())
                {
                    currentCursor = 1;
                    currentSlot = 0;
                    curseurReliques.SetActive(false);
                    curseurPause.SetActive(true);
                    isInReliques = false;
                }
            }

            if (isInReliques == false && menuPause.active == true && isPaused && (playerOneController.VerticalAxisRaw() != 0))
            {
                if (playerOneController.VerticalAxisRaw() < 0 && currentCursor < (listMenuPause.Count - 1) && canMoveCursor)
                {
                    currentCursor++;
                    curseurPause.transform.localPosition = listMenuPause[currentCursor].localPosition;
                    canMoveCursor = false;
                    StartCoroutine(MoveCursor());
                }
                else if (playerOneController.VerticalAxisRaw() > 0 && currentCursor > 1 && canMoveCursor)
                {
                    currentCursor--;
                    curseurPause.transform.localPosition = listMenuPause[currentCursor].localPosition;
                    canMoveCursor = false;
                    StartCoroutine(MoveCursor());
                }
                else if (playerOneController.VerticalAxisRaw() > 0 && currentCursor == 1 && canMoveCursor)
                {
                    currentCursor = 4;
                    curseurPause.transform.localPosition = listMenuPause[currentCursor].localPosition;
                    canMoveCursor = false;
                    StartCoroutine(MoveCursor());
                }
                if (playerOneController.VerticalAxisRaw() < 0 && currentCursor == (listMenuPause.Count - 1) && canMoveCursor)
                {
                    currentCursor = 1;
                    curseurPause.transform.localPosition = listMenuPause[currentCursor].localPosition;
                    canMoveCursor = false;
                    StartCoroutine(MoveCursor());
                }
            }
            else if (isInReliques == false && menuPause.active == true && isPaused && (playerOneController.HorizontalAxisRaw() != 0))
            {
                if(playerOneController.HorizontalAxisRaw() < 0 && canMoveCursor)
                {
                    isInReliques = true;
                    curseurPause.SetActive(false);
                    curseurReliques.SetActive(true);
                    currentSlot = 0;
                    curseurReliques.transform.position = listMenuReliques[currentSlot].position;
                    canMoveCursor = false;
                    StartCoroutine(MoveCursor());
                }
                else if (playerOneController.HorizontalAxisRaw() > 0 && canMoveCursor)
                {
                    isInReliques = true;
                    curseurPause.SetActive(false);
                    curseurReliques.SetActive(true);
                    currentSlot = 0;
                    curseurReliques.transform.position = listMenuReliques[currentSlot].position;
                    canMoveCursor = false;
                    StartCoroutine(MoveCursor());
                }
            }
            if (playerOneController.ButtonA() && isInReliques == false && isPaused && menuPause.active == true)
            {
                switch (currentCursor)
                {
                    case 1:
                        pause();
                        currentCursor = 1;
                        break;
                    case 2:
                        currentCursor = 1;
                        break;
                    case 3:
                        currentCursor = 1;
                        break;
                    case 4:
                        menuQuitter.SetActive(true);
                        menuPause.SetActive(false);
                        menuReliques.SetActive(false);
                        menuStats.SetActive(false);
                        currentCursor = 1;
                        break;
                }
            }

            if (menuQuitter.active == true && isPaused && (playerOneController.HorizontalAxisRaw() != 0))
            {
                if (playerOneController.HorizontalAxisRaw() != 0 && currentCursor == 2  && canMoveCursor)
                {
                    currentCursor--;
                    curseurQuitter.transform.localPosition = listMenuQuitter[currentCursor].localPosition;
                    canMoveCursor = false;
                    StartCoroutine(MoveCursor());
                }
                if (playerOneController.HorizontalAxisRaw() != 0 && currentCursor == 1 && canMoveCursor)
                {
                    currentCursor++;
                    curseurQuitter.transform.localPosition = listMenuQuitter[currentCursor].localPosition;
                    canMoveCursor = false;
                    StartCoroutine(MoveCursor());
                }

            }
            if (playerOneController.ButtonA() && isPaused && menuQuitter.active == true)
            {  
                    switch (currentCursor)
                    {
                        case 1:
                            Application.Quit();
                            break;
                        case 2:
                            menuQuitter.SetActive(false);
                            menuPause.SetActive(true);
                            menuReliques.SetActive(true);
                            menuStats.SetActive(true);
                            currentCursor = 5;
                            break;
                        default:
                            break;

                    }                
            }
            else if(playerOneController.ButtonB() && isPaused && menuQuitter.active == true)
                {
                menuQuitter.SetActive(false);
                menuPause.SetActive(true);
                menuReliques.SetActive(true);
                menuStats.SetActive(true);
                currentCursor = 5;
            
                }
        }
    }

    void pause()
    {
        if (gonePaused)
        {
            StartCoroutine(PauseCoroutine(true));
            gonePaused = false;
            menuPause.SetActive(true);
            menuReliques.SetActive(true);
            menuStats.SetActive(true);
            isPaused = true;
        }
        else
        {
            StartCoroutine(PauseCoroutine(false));
            gonePaused = true;
            menuPause.SetActive(false);
            menuReliques.SetActive(false);
            menuStats.SetActive(false);
            isPaused = false;
        }
    }

    IEnumerator PauseCoroutine(bool state)
    {
        float timeScaleObjective;

        if (state)
        {
            timeScaleObjective = 0f;
            Time.timeScale = 1f;
        }
        else
        {
            timeScaleObjective = 1f;
            Time.timeScale = 0f;
        }
            

        
        for (int i = 0; i < forTime; i++)
        {
            if (Time.timeScale < 0f)
            {
                Time.timeScale = 0f;
            }
            else if(Time.timeScale > 1f)
            {
                Time.timeScale = 1f;
            }
            else if (state)
            {
                Time.timeScale -= 1 / forTime;
            }
            else
            {
                Time.timeScale += 1 / forTime;
            }

            yield return new WaitForSeconds(pauseFadeDuration / forTime);
        }


        if (state)
        {
            Time.timeScale = 0f;
            
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void RelicRender(List<GameObject> biteTheDust)
    {
        for(int i = 0; i<biteTheDust.Count; i++)
        {
            biteTheDust[i].transform.SetParent(listMenuReliques[i].GetChild(0));
        }
    }

    IEnumerator MoveCursor()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        canMoveCursor = true;
    }
}

