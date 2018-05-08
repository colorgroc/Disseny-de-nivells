using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {
    public int type;
	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            if (type == 1)
                Movement.fasterPickUp = true;
            else if (type == 2)
                Movement.freezePickUp = true;
            else if (type == 3)
                Control.gameTime += 10;
                //afegir temps al hud
            Destroy(this.gameObject);
        }
    }
}
