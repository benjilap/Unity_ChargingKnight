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
    List<Transform> listMenuPause = new List<Transform>();
    GameObject curseur;
    GameObject player;
    PlayerController playerOneController;
    PlayerController playerTwoController;
    int currentCursor;

    // Use this for initialization
    void Start () {
        gonePaused = false;
        menuPause = GameObject.Find("MenuPause");
        Time.timeScale = 0f;
        for(int i = 0; i < menuPause.transform.childCount; i++ )
        {
            
            listMenuPause.Add(menuPause.transform.GetChild(i));
           
        }
        listMenuPause.RemoveAt(0);
        curseur = listMenuPause[0].gameObject;
        if(GameObject.Find("Player1") && GameObject.Find("Player2") !=null)
        {
            playerOneController = GameObject.Find("Player1").GetComponent<PlayerController>();
            playerTwoController = GameObject.Find("Player2").GetComponent<PlayerController>();
        }
        currentCursor = 1;
        menuPause.active = false;

    }

    // Update is called once per frame
    void Update()
    {



        if (Input.GetKeyDown(KeyCode.C))
            pause();

        if (isPaused && playerOneController.VerticalAxis() != 0)
        {
            if(playerController.VerticalAxis() < 0 && currentCursor < listMenuPause.Count)
            {
                currentCursor--;
                curseur.transform.localPosition = listMenuPause[currentCursor].localPosition;
            }
            if(playerController.VerticalAxis() > 0 && currentCursor > 1)
            {
                currentCursor++;
                curseur.transform.localPosition = listMenuPause[currentCursor].localPosition;
            }
        }
    


    }

    void pause()
    {
        if (gonePaused)
        {
            StartCoroutine(PauseCoroutine(false));
            gonePaused = false;
            menuPause.active = true;
        }
        else
        {
            StartCoroutine(PauseCoroutine(true));
            gonePaused = true;
            menuPause.active = false;
        }
    }

    IEnumerator PauseCoroutine(bool state)
    {
        float timeScaleObjective;
        Debug.Log(state);
        Debug.Log(Time.timeScale);

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
}

