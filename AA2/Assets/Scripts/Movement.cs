using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    [SerializeField]
    private float velX;//, velY;
    private bool grounded, click;
    private float temps;
    public static bool isOnAPlatform = false;
	// Use this for initialization
	void Start () {
        //vel = this.gameObject.GetComponent<Rigidbody2D>().velocity.x;
    }

    // Update is called once per frame
    void Update () {
        //this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(1, 0, 0), ForceMode2D.Force);
        // this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(vel, 0, 0);

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
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && grounded)
                this.GetComponent<Rigidbody2D>().gravityScale *= -1;
            //Touch[] touches = Input.touches;
            else if (touch.phase == TouchPhase.Stationary)
                this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            else
            {
                if (!isOnAPlatform)
                    this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(velX, 0, 0);
                else
                    this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            }
        }

        //pc
        if (Input.GetMouseButtonDown(0) && grounded)
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
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(velX, 0);
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

