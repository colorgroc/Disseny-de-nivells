﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (this.transform.position.x < this.transform.parent.position.x)
            this.GetComponent<SpriteRenderer>().flipX = true;
        else this.GetComponent<SpriteRenderer>().flipX = false;

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            //die
            Destroy(coll.gameObject);
            //show restart menu
        }
    }
}
