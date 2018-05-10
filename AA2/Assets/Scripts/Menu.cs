using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    private bool god;
    public Toggle godMode;
    public AudioSource sons;
    [SerializeField]
    private AudioClip accept, back;
    private void Awake()
    {
        //not working properly gsg
        if (PlayerPrefs.GetInt("God") == 0)
            god = false;
        else if (PlayerPrefs.GetInt("God") == 1)
            god = true;
        if (god)
            godMode.isOn = true;
        else if (!god)
           godMode.isOn = false;
    }
    public void Tutorial()
    {
        sons.PlayOneShot(accept);
        Control.paused = false;
        SceneManager.LoadScene("Tutorial");

    }
    public void Level1()
    {
        sons.PlayOneShot(accept);
        Control.paused = false;
        SceneManager.LoadScene("Level1");
    }
    public void GodModeChange()
    {
        god = !god;
        if (god) GodModeOn();
        else if (!god) GodModeOff();
    }
    private void GodModeOn()
    {
        sons.PlayOneShot(accept);
        PlayerPrefs.SetInt("God", 1);
        //Control.godMode = true;
    }
    private void GodModeOff()
    {
        sons.PlayOneShot(back);
        PlayerPrefs.SetInt("God", 0);
        //Control.godMode = false;
    }
    public void Exit()
    {
        sons.PlayOneShot(accept);
        Application.Quit();
    }
}
