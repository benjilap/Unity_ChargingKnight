using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

    [SerializeField]
    int forTime;
    bool gonePaused;
    bool isPaused;
    [SerializeField]
    float pauseFadeDuration = 0.3f;
    GameObject menuPause;
    GameObject menuQuitter;
    GameObject menuReliques;
    List<Transform> listMenuPause = new List<Transform>();
    List<Transform> listMenuQuitter = new List<Transform>();
    GameObject curseurPause;
    GameObject curseurQuitter;
    GameObject player;
    PlayerController playerOneController;
    PlayerController playerTwoController;
    int currentCursor;
    bool canMoveCursor;
    [SerializeField]
    int nombreSlots;
    Object slotPrefab;

    // Use this for initialization
    void Start () {
        slotPrefab = Resources.Load("Reliques/Slot");
        canMoveCursor = true;
        gonePaused = false;
        isPaused = false;
        menuPause = GameObject.Find("MenuPause");
        menuQuitter = GameObject.Find("MenuQuitter");
        menuReliques = GameObject.Find("MenuReliques");
        Time.timeScale = 0f;
        for(int i = 0; i < menuPause.transform.childCount; i++ )
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
        menuQuitter.active = false;

        curseurPause = listMenuPause[0].gameObject;
        if (GameObject.Find("Player1") && GameObject.Find("Player2") !=null)
        {
            playerOneController = GameObject.Find("Player1").GetComponent<PlayerController>();
            playerTwoController = GameObject.Find("Player2").GetComponent<PlayerController>();
        }
        else
        {
            playerOneController = GameObject.Find("Player1").GetComponent<PlayerController>();
            playerTwoController = null;
        }
        for(int i = 0; i < nombreSlots; i++)
        {
            Instantiate(slotPrefab, menuReliques.transform);
        }
        currentCursor = 1;
        menuPause.active = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (playerOneController && playerTwoController != null)
        {
            if (playerOneController.Start() || playerTwoController.Start())
                pause();

            if (menuPause.active == true && isPaused && (playerOneController.VerticalAxisRaw() != 0 || playerOneController.VerticalAxisRaw() != 0))
            {
                if ((playerOneController.VerticalAxisRaw() < 0 || playerTwoController.VerticalAxisRaw() > 0) && currentCursor < (listMenuPause.Count - 1) && canMoveCursor)
                {
                    currentCursor++;
                    curseurPause.transform.position = listMenuPause[currentCursor].position;
                    canMoveCursor = false;
                    StartCoroutine(MoveCursor());
                }
                if ((playerOneController.VerticalAxisRaw() > 0 || playerTwoController.VerticalAxisRaw() > 0) && currentCursor > 1 && canMoveCursor)
                {
                    currentCursor--;
                    curseurPause.transform.position = listMenuPause[currentCursor].position;
                    canMoveCursor = false;
                    StartCoroutine(MoveCursor());
                }
            }
            if ((playerOneController.ButtonA() || playerTwoController.ButtonA()) && isPaused && menuPause.active == true)
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
                        menuQuitter.active = true;
                        menuPause.active = false;
                        currentCursor = 1;
                        break;
                }
            }
        }
        else
        {
            if (playerOneController.Start())
                pause();

            if (menuPause.active == true && isPaused && (playerOneController.VerticalAxisRaw() != 0))
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
            if (playerOneController.ButtonA() && isPaused && menuPause.active == true)
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
                        menuQuitter.active = true;
                        menuPause.active = false;
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
                            menuQuitter.active = false;
                            menuPause.active = true;
                            currentCursor = 5;
                            break;
                        default:
                            break;

                    }                
            }
            else if(playerOneController.ButtonB() && isPaused && menuQuitter.active == true)
                {
                menuQuitter.active = false;
                menuPause.active = true;
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
            menuPause.active = true;
            isPaused = true;
        }
        else
        {
            StartCoroutine(PauseCoroutine(false));
            gonePaused = true;
            menuPause.active = false;
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

    IEnumerator MoveCursor()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        canMoveCursor = true;
    }
}

