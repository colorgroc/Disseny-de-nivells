using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {
    public int type;
    [SerializeField]
    private AudioClip freeze, time, faster;
    // Use this for initialization
    void Start () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            if (type == 1)
            {
                Control.sons.PlayOneShot(faster);
                Control.fasterPickUp = true;
                Control.timeFaster = 3;
            }
            else if (type == 2)
            {
                Control.sons.PlayOneShot(freeze);
                Control.freezePickUp = true;
                Control.timeFreeze = 3;
            }
            else if (type == 3)
            {
                Control.sons.PlayOneShot(time);
                Control.gameTime += 10;
            }
                //afegir temps al hud
            Destroy(this.gameObject);
        }
    }
}
