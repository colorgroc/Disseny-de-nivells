using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public void Tutorial()
    {
        Control.paused = false;
        SceneManager.LoadScene("Tutorial");
        
    }
    public void Level1()
    {
        Control.paused = false;
        SceneManager.LoadScene("Level1");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
