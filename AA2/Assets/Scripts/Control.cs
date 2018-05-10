using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Control : MonoBehaviour {

    public Canvas pause, restart, winner;
    public Text temps;
    public Image freeze, faster;
    public Button pauseButton;
    public static bool paused, godMode;
    public static float gameTime, timeFaster, timeFreeze;
    public static AudioSource sons;
    public AudioSource music;
    [SerializeField]
    private AudioClip accept, back, loser, win;
    private bool once;
    public float timeGame;

    public static bool won, freezePickUp, timePickUp, fasterPickUp;
	// Use this for initialization
	void Awake () {
        pause.GetComponent<Canvas>().enabled = false;
        restart.GetComponent<Canvas>().enabled = false;
        winner.GetComponent<Canvas>().enabled = false;
        sons = GameObject.Find("Sons").GetComponent<AudioSource>();
        freeze.enabled = false;
        faster.enabled = false;
        won = paused = freezePickUp = timePickUp = fasterPickUp = false;
        timeFaster = timeFreeze = 0;
        gameTime = timeGame;
        if (PlayerPrefs.GetInt("God") == 1)
            godMode = true;
        else if(PlayerPrefs.GetInt("God") == 0)
            godMode = false;
        once = false;
        //Debug.Log(godMode);
    }
    private void Start()
    {
        timeFaster = timeFreeze = 3;
        Time.timeScale = 1;
    }
    // Update is called once per frame
    void Update () {

        if (Input.GetKeyUp(KeyCode.Escape))
            paused = true;
        if (paused)
            Pause();
        if(gameTime > 0)
            gameTime -= Time.deltaTime;
        temps.text = GetMinutes(gameTime);

        if (won)
        {
            music.Stop();
            if (!once)
            {
                sons.PlayOneShot(win);
                once = true;
            }
            Time.timeScale = 0;
            winner.GetComponent<Canvas>().enabled = true;
        }

        if (gameTime <= 0 && !won && !godMode)
        {
            music.Stop();
            if (!once)
            {
                sons.PlayOneShot(loser);
                once = true;
            }
            restart.GetComponent<Canvas>().enabled = true;
            Time.timeScale = 0;
        }


        //pickUps

        faster.enabled = fasterPickUp;
        freeze.enabled = freezePickUp;

        if (fasterPickUp)
        {
            //faster.enabled = true;
            timeFaster -= Time.deltaTime;
            faster.fillAmount = timeFaster/3;
            if (timeFaster <= 0)
            {
                fasterPickUp = false;
                //faster.enabled = false;
            }
        }
        if (freezePickUp)
        {
            //freeze.enabled = true;
            timeFreeze -= Time.deltaTime;
            freeze.fillAmount = timeFreeze/3;
            if (timeFreeze <= 0)
            {
                freezePickUp = false;
                //freeze.enabled = false;
            }
        }
    }
    public void Pause()
    {
        music.Pause();
        sons.PlayOneShot(accept);
        pause.GetComponent<Canvas>().enabled = true;
        Time.timeScale = 0;
        pauseButton.interactable = false;
    }
    public void UnPause()
    {
        music.UnPause();
        sons.PlayOneShot(back);
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
        sons.PlayOneShot(accept);
        SceneManager.LoadScene("Menu");
    }
    //poner funcion tiempo biped seek
    private string GetMinutes(float timeLeft)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeLeft);
        return string.Format("{0:0}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
    }

    public void NextLevel()
    {
        sons.PlayOneShot(accept);
        //cargar el nivell seguent, en aquest cas el level 1
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Restart()
    {
        sons.PlayOneShot(accept);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
