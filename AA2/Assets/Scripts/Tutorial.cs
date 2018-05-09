using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {
    [SerializeField]
    private AudioClip accept;
    // Use this for initialization
    void Start () {
        Time.timeScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0))
        {
            this.gameObject.SetActive(false);
            Time.timeScale = 1;
            Control.sons.PlayOneShot(accept);
        }
	}
}
