using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour {
    public GameObject iPoint, fPoint, startPoint;
    public bool platHorizontal, enemy;
    bool right, down;
    public float vel, freezedVel;
    [SerializeField]
    private AudioClip die, loser;
    // Use this for initialization
    void Start()
    {
        if (platHorizontal)
        {
            if (startPoint == null)
            {
                this.transform.position = new Vector3(this.transform.position.x, iPoint.transform.position.y, this.transform.position.z);
                right = true;
            }
            else
            {
                this.transform.position = new Vector3(this.transform.position.x, startPoint.transform.position.y, this.transform.position.z);
                if (startPoint == iPoint)
                    right = true;
                else right = false;
            }
        }
        else
        {
            if (startPoint == null)
            {
                this.transform.position = new Vector3(this.transform.position.x, iPoint.transform.position.y, this.transform.position.z);
                down = true;
            }
            else
            {
                this.transform.position = new Vector3(this.transform.position.x, startPoint.transform.position.y, this.transform.position.z);
                if (startPoint == iPoint)
                    down = true;
                else down = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (platHorizontal)
        {
            if (right)
            {
                if (enemy)
                {
                    if (!Control.freezePickUp)
                        this.transform.position = new Vector3(this.transform.position.x + vel, this.transform.position.y, this.transform.position.z);
                    else this.transform.position = new Vector3(this.transform.position.x + freezedVel, this.transform.position.y, this.transform.position.z);
                }
                else this.transform.position = new Vector3(this.transform.position.x + vel, this.transform.position.y, this.transform.position.z);
            }
            else
            {
                if (enemy)
                {
                    if (!Control.freezePickUp)
                        this.transform.position = new Vector3(this.transform.position.x - vel, this.transform.position.y, this.transform.position.z);
                    else this.transform.position = new Vector3(this.transform.position.x - freezedVel, this.transform.position.y, this.transform.position.z);
                }
                else this.transform.position = new Vector3(this.transform.position.x - vel, this.transform.position.y, this.transform.position.z);
            }

            if (this.transform.position.x <= iPoint.transform.position.x && !right)
                right = !right;

            else if (this.transform.position.x >= fPoint.transform.position.x && right)
                right = !right;
        }
        else
        {
            if (down)
            {
                if (enemy)
                {
                    if (!Control.freezePickUp)
                        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - vel, this.transform.position.z);
                    else this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - freezedVel, this.transform.position.z);
                }
                else this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - vel, this.transform.position.z);
            }
            else
            {
                if (enemy)
                {
                    if (!Control.freezePickUp)
                        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + vel, this.transform.position.z);
                    else this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + freezedVel, this.transform.position.z);
                } else this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + vel, this.transform.position.z);
            }

            if (this.transform.position.y >= iPoint.transform.position.y && !down)
                down = !down;

            else if (this.transform.position.y <= fPoint.transform.position.y && down)
                down = !down;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemy && collision.gameObject.tag == "Player")
        {
            if (!Control.godMode)
            {
                Destroy(collision.gameObject);
                Control.sons.PlayOneShot(die);
                GameObject.Find("Control").GetComponent<Control>().restart.enabled = true;
                Time.timeScale = 0;
                GameObject.Find("Control").GetComponent<Control>().music.Stop();
                Control.sons.PlayOneShot(loser);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (!enemy)
            {
                coll.transform.parent = this.transform;
                Movement.isOnAPlatform = true;
            }
        }
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (!enemy)
            {
                //show restart canvas
                coll.transform.parent = null;
                Movement.isOnAPlatform = false;
            }
        }
    }

}


