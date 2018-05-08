using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour {

    public Canvas pause;
    public Button pauseButton;
    public static bool paused;
	// Use this for initialization
	void Awake () {
        pause.GetComponent<Canvas>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Escape))
            SetPauseBool();
        if (paused)
            Pause();
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
}
