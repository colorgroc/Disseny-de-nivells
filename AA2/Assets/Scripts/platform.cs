using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour {
    public GameObject iPoint, fPoint;
    public bool platHorizontal, enemy;
    bool right, down;
    public float vel, freezedVel;
    [SerializeField]
    private AudioClip die;
    // Use this for initialization
    void Start()
    {
        if (platHorizontal)
        {
            this.transform.position = new Vector3(iPoint.transform.position.x, this.transform.position.y, this.transform.position.z);
            right = true;
        }
        else
        {
            this.transform.position = new Vector3(this.transform.position.x, iPoint.transform.position.y, this.transform.position.z);
            down = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (platHorizontal)
        {
            if (right)
            {
                if(!Control.freezePickUp)
                    this.transform.position = new Vector3(this.transform.position.x + vel, this.transform.position.y, this.transform.position.z);
                else this.transform.position = new Vector3(this.transform.position.x + freezedVel, this.transform.position.y, this.transform.position.z);
            }
            else
            {
                if (!Control.freezePickUp)
                    this.transform.position = new Vector3(this.transform.position.x - vel, this.transform.position.y, this.transform.position.z);
                else this.transform.position = new Vector3(this.transform.position.x - freezedVel, this.transform.position.y, this.transform.position.z);
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
                if (!Control.freezePickUp)
                    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - vel, this.transform.position.z);
                else this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - freezedVel, this.transform.position.z);
            }
            else
            {
                if (!Control.freezePickUp)
                    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + vel, this.transform.position.z);
                else this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + freezedVel, this.transform.position.z);
            }

            if (this.transform.position.y >= iPoint.transform.position.y && !down)
                down = !down;

            else if (this.transform.position.y <= fPoint.transform.position.y && down)
                down = !down;
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
            else
            {
                if(!Control.godMode)
                    Destroy(coll.gameObject);
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
            else
            {
                //show restart canvas
                if (!Control.godMode)
                {
                    Destroy(coll.gameObject);
                    Control.sons.PlayOneShot(die);
                }
            }
            
        }
    }

}


