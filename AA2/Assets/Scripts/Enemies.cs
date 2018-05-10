using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour {
    public int type;
    public GameObject iPoint, fPoint, startPoint;
    bool right;
    public float vel, freezedVel;
    public GameObject eBullet;
    public float bulletSpeed, freezedBulletSpeed;
    public float delay;
    public float attackSpeed, freezeAttackSpeed;
    [SerializeField]
    private AudioClip die;
    //private int bulletCount = 0;
    private GameObject player = null;
    // Use this for initialization
    void Start () {

        if (type == 2)
        {
            if (startPoint == null)
            {
                this.transform.position = new Vector3(iPoint.transform.position.x, this.transform.position.y, this.transform.position.z);
                right = true;
            }
            else
            {
                this.transform.position = new Vector3(startPoint.transform.position.x, this.transform.position.y, this.transform.position.z);
                if (startPoint == iPoint)
                    right = true;
                else right = false;
            }
        }

    }

	
	// Update is called once per frame
	void Update () {
        if (type == 2)
        {
            if (right) {
                if (!Control.freezePickUp)
                    this.GetComponent<Rigidbody2D>().velocity = new Vector2(vel, 0);
                else this.GetComponent<Rigidbody2D>().velocity = new Vector2(freezedVel, 0);
            }
            else {
                if (!Control.freezePickUp)
                    this.GetComponent<Rigidbody2D>().velocity = new Vector2(-vel, 0);
                else this.GetComponent<Rigidbody2D>().velocity = new Vector2(-freezedVel, 0);
            }

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
            if (!Control.godMode)
            {
                Control.sons.PlayOneShot(die);
                Destroy(coll.gameObject);
                GameObject.Find("Control").GetComponent<Control>().restart.enabled = true;
                GameObject.Find("Control").GetComponent<Control>().music.Stop();
            }
            //show restart menu
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player" && type == 3)
        {
            player = coll.gameObject;
            if (!Control.paused)
            {
                if(!Control.freezePickUp)
                    InvokeRepeating("Shoot", delay, attackSpeed);
                else InvokeRepeating("Shoot", delay, freezeAttackSpeed);
            }
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
        if (dir.x < 0) {
            if (!Control.freezePickUp)
                shoot.GetComponent<Rigidbody2D>().velocity = new Vector3(-bulletSpeed, 0, 0);
            else shoot.GetComponent<Rigidbody2D>().velocity = new Vector3(-freezedBulletSpeed, 0, 0);
        }
        else {
            if (!Control.freezePickUp)
                shoot.GetComponent<Rigidbody2D>().velocity = new Vector3(bulletSpeed, 0, 0);
            else shoot.GetComponent<Rigidbody2D>().velocity = new Vector3(freezedBulletSpeed, 0, 0);
        }
        
    }

}

