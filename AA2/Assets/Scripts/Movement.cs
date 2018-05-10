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
    [SerializeField]
    private AudioClip jump;
    // Use this for initialization
    void Start () {
        //vel = this.gameObject.GetComponent<Rigidbody2D>().velocity.x;
    }

    // Update is called once per frame
    void Update () {
  
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
            //Control.sons.PlayOneShot(jump);
        }

        if (Input.GetMouseButtonDown(0) && grounded && !Control.paused)
        {
            temps = Time.time;
            click = true;
        }

        if (click && (Time.time - temps) > 0.15)
        {
            this.GetComponent<Animator>().SetBool("Stop", true);
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        else
        {
            if (grounded)// && !isOnAPlatform)
            {
                if (!Control.fasterPickUp)
                    this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(velX, 0, 0);
                else if (Control.fasterPickUp)
                    this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(velPowerUp, 0, 0);
                this.GetComponent<Animator>().SetBool("Stop", false);               
            }
            else
            {
                this.GetComponent<Animator>().SetBool("Stop", true);
                this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }

        if (Input.GetMouseButtonUp(0) && grounded && !Control.paused)
        {
            click = false;

            if ((Time.time - temps) < 0.15)
            {
                Control.sons.PlayOneShot(jump);
                // this.GetComponent<Animator>().SetBool("Jump", true);
                this.GetComponent<Rigidbody2D>().gravityScale *= -1;
                this.transform.GetComponent<SpriteRenderer>().flipY = !this.transform.GetComponent<SpriteRenderer>().flipY;
            }
        }
    }
}

