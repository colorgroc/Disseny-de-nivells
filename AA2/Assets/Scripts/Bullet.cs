using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField]
    private AudioClip die, loser;
    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
        if (this.transform.position.x < this.transform.parent.position.x)
            this.GetComponent<SpriteRenderer>().flipX = true;
        else this.GetComponent<SpriteRenderer>().flipX = false;
        if (this.gameObject.transform.position.x >= this.gameObject.transform.parent.transform.position.x + 9 || this.gameObject.transform.position.x <= this.gameObject.transform.parent.transform.position.x - 9)
            Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            //die
            if (!Control.godMode)
            {
                Control.sons.PlayOneShot(die);
                Destroy(coll.gameObject);
                GameObject.Find("Control").GetComponent<Control>().restart.enabled = true;
                GameObject.Find("Control").GetComponent<Control>().music.Stop();
                Control.sons.PlayOneShot(loser);
            }
            //show restart menu
        }
        else Destroy(this.gameObject);
    }
}
