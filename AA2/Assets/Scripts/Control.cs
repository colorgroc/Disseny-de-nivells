using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour {

    public Canvas pause, restart, winner;
    public Button pauseButton;
    public static bool paused;
    public static float gameTime;
    public static bool won;
	// Use this for initialization
	void Awake () {
        pause.GetComponent<Canvas>().enabled = false;
        restart.GetComponent<Canvas>().enabled = false;
        winner.GetComponent<Canvas>().enabled = false;

        gameTime = 120f;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyUp(KeyCode.Escape))
            SetPauseBool();
        if (paused)
            Pause();

       
        gameTime -= Time.deltaTime;
        if(gameTime <= 0)
        {
            if (won)
            {
                winner.GetComponent<Canvas>().enabled = true;
                //congratulations canvas!
            }
            else
            {
                restart.GetComponent<Canvas>().enabled = true;
                //restart canvas
            }
        }
	}
    public void Pause()
    {
        pause.GetComponent<Canvas>().enabled = true;
        Time.timeScale = 0;
        pauseButton.interactable = false;
    }
    public void UnPause()
    {
        pause.GetComponent<Canvas>().enabled = false;
        Time.timeScale = 1;
        pauseButton.interactable = true;
    }
    public void SetPauseBool()
    {
        paused = !paused;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    //poner funcion tiempo biped seek
}
