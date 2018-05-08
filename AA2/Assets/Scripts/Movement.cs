using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    [SerializeField]
    private float velX;//, velY;
    public float velPowerUp;
    private bool grounded, click;
    private float temps;
    public static bool isOnAPlatform = false;
    public static  bool timePickUp, freezePickUp, fasterPickUp;
    public static float timeFaster, timeFreeze;
	// Use this for initialization
	void Start () {
        //vel = this.gameObject.GetComponent<Rigidbody2D>().velocity.x;
    }

    // Update is called once per frame
    void Update () {
        Debug.Log(fasterPickUp);
        //pickUps
        if (fasterPickUp)
        {
            
            timeFaster += Time.deltaTime;
            if(timeFaster >= 3)
            {
                fasterPickUp = false;
            }
        }

        if (this.gameObject.GetComponent<Rigidbody2D>().IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            this.grounded = true;
            this.GetComponent<Animator>().SetBool("Jump", false);
            //this.GetComponent<Animator>().SetBool("Run", true);
        }
        else
        {
            this.grounded = false;
            this.GetComponent<Animator>().SetBool("Stop", false);
            this.GetComponent<Animator>().SetBool("Jump", true);
        }

        //android
        if (Input.touchCount == 1 && !Control.paused)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && grounded) {
                this.GetComponent<Rigidbody2D>().gravityScale *= -1;
                this.transform.GetComponent<SpriteRenderer>().flipY = !this.transform.GetComponent<SpriteRenderer>().flipY;
            }
            //Touch[] touches = Input.touches;
            else if (touch.phase == TouchPhase.Stationary)
            {
                this.GetComponent<Animator>().SetBool("Stop", true);
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
            else
            {
                if (grounded && !isOnAPlatform)
                {
                    if (!fasterPickUp)
                        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(velX, 0, 0);
                    else if (fasterPickUp)
                        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(velPowerUp, 0, 0);
                    this.GetComponent<Animator>().SetBool("Stop", false);

                }
                else
                {
                    this.GetComponent<Animator>().SetBool("Stop", true);
                    this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                }
            }
        }

        //pc
        if (Input.GetMouseButtonDown(0) && grounded && !Control.paused)
        {
            temps = Time.time;
            click = true;
        }

        if (click && (Time.time - temps) > 0.15)
        {
            this.GetComponent<Animator>().SetBool("Stop", true);
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        else
        {
            if (grounded && !isOnAPlatform)
            {
                if (!fasterPickUp)
                    this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(velX, 0, 0);
                else if (fasterPickUp)
                    this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(velPowerUp, 0, 0);
                this.GetComponent<Animator>().SetBool("Stop", false);
                
            }
            else
            {
                this.GetComponent<Animator>().SetBool("Stop", true);
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
        }

        if (Input.GetMouseButtonUp(0) && grounded && !Control.paused)
        {
            click = false;

            if ((Time.time - temps) < 0.15)
            {
               // this.GetComponent<Animator>().SetBool("Jump", true);
                this.GetComponent<Rigidbody2D>().gravityScale *= -1;
                this.transform.GetComponent<SpriteRenderer>().flipY = !this.transform.GetComponent<SpriteRenderer>().flipY;
            }
        }
    }
}

