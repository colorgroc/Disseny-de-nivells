using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour {
    public int type;
    public GameObject iPoint, fPoint;
    bool right;
    public float vel;
    public GameObject eBullet;
    public float bulletSpeed;
    public float delay;
    public float atackSpeed;
    private int bulletCount = 0;
    private GameObject player = null;
    // Use this for initialization
    void Start () {

        if (type == 2)
        {
            this.transform.position = new Vector3(iPoint.transform.position.x, this.transform.position.y, this.transform.position.z);
            right = true;
        }
        //else if (type == 3)
        //{

        //}
    }

	
	// Update is called once per frame
	void Update () {
        if (type == 2)
        {
            if (right)
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(vel, 0);
            else
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(-vel, 0);

            if (this.transform.position.x <= iPoint.transform.position.x && !right)
                right = !right;

            else if (this.transform.position.x >= fPoint.transform.position.x && right)
                right = !right;
        }
   
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

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player" && type == 3)
        {
            player = coll.gameObject;
            if(!Control.paused)
                InvokeRepeating("Shoot", delay, atackSpeed);
        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player" && type == 3)
        {
            player = null;
            if (!Control.paused)
                CancelInvoke("Shoot");
        }
    }

    void Shoot()
    {
        GameObject shoot = Instantiate(eBullet, this.transform.position, this.transform.rotation);
        shoot.transform.parent = this.transform;
        Vector3 dir = (player.transform.position - this.transform.position).normalized;
        if(dir.x < 0) 
            shoot.GetComponent<Rigidbody2D>().velocity = new Vector3(-bulletSpeed, 0, 0);
        else
            shoot.GetComponent<Rigidbody2D>().velocity = new Vector3(bulletSpeed, 0, 0);
        
    }

}

