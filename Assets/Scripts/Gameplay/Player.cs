using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool privateMessage = false;
    public bool activeGame = true;
    public bool AI = false;
    public bool singleplayer = false;

    private Rigidbody2D rb;
    private float baseForce = 500;

    private float screenHeight = 0;
    private float screenWidth = 0;

    public bool shootS = false;
    public bool shootE = false;
    public bool mouseDown = false;
    public bool ropeBouncer = false;           //Let the balls add force just once when they hit ropes without player's movement

    private Vector2 intPos;         //Referance positions in order to shoot ball whereever we want
    private Vector2 lastPos;
    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        GameObject menu = GameObject.FindGameObjectWithTag("Menu");             //Checks if the game is singleplayer or not
        singleplayer = menu.gameObject.GetComponent<MainMenuControls>().singleplayer;

        rb = GetComponent<Rigidbody2D>();
        screenHeight = Screen.height;
        screenWidth = Screen.width;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(intPos);
        //Debug.Log(lastPos);
        //Debug.Log(shoot);
        //rb.velocity -= new Vector2(0.1f, 0.1f);

        if (privateMessage)
        {
            //Object spesific messages here
        }

        if (!mouseDown && !ropeBouncer)             //Blocks balls from staying inside the rope
        {
            if (shootE)
                rb.AddForce(new Vector2(0, -20));
            else if (shootS) 
                rb.AddForce(new Vector2(0, 20));


        }
    }


    private Vector3 offset;
    void OnMouseDown()
    {
        if (activeGame && !AI)
        {
            mouseDown = true;
            rb.velocity = new Vector2(0, 0);
            offset = gameObject.transform.position -
                Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
        }
    }
    void OnMouseDrag()
    {
        if (activeGame && !AI)
        {
            Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
            rb.MovePosition(Camera.main.ScreenToWorldPoint(newPosition) + offset);
        }
    }
    private void OnMouseUp()          
    {
        if (activeGame && !AI)
        {
            if (shootS)             //This and the code below is about shooting the ball
            {
                lastPos = transform.position;
                float dirX = intPos.x - lastPos.x;
                float dirY = intPos.y - lastPos.y;
                float dirMag = Mathf.Sqrt((dirX * dirX) + (dirY * dirY));
                dirX /= dirMag;
                dirY /= dirMag;
                direction = new Vector2(dirX * baseForce, dirY * baseForce);
                rb.AddForce(new Vector2(0, 0) + direction);                   //Force that applying to ball while shooting   
            }
            else if (shootE)
            {
                lastPos = transform.position;
                float dirX = intPos.x - lastPos.x;
                float dirY = intPos.y - lastPos.y;
                float dirMag = Mathf.Sqrt((dirX * dirX) + (dirY * dirY));
                dirX /= dirMag;
                dirY /= dirMag;
                direction = new Vector2(dirX * baseForce, dirY * baseForce);
                rb.AddForce(new Vector2(0, 0) + direction);                   //Force that applying to ball while shooting     
            }

            Invoke("mouseDownFalse", 0.1f);         //Prohibits ball to gain extra velocity (related to blocking balls from staying inside the rope)
        }
    }

    private void mouseDownFalse()           
    {
        mouseDown = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "RopeS")
        {
            shootS = true;
            ropeBouncer = false;
            intPos = transform.position;
        }else if(collision.gameObject.tag == "RopeE")
        {
            shootE = true;
            ropeBouncer = false;
            intPos = transform.position;
        }

        if(singleplayer && collision.gameObject.tag == "AreaE")
        {
            AI = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RopeS")
        {
            lastPos = new Vector2(0,0);
            intPos = new Vector2(0,0);
            shootS = false;
            ropeBouncer = true;
        }else if(collision.gameObject.tag == "RopeE")
        {
            lastPos = new Vector2(0, 0);
            intPos = new Vector2(0, 0);
            shootE = false;
            ropeBouncer = true;
        }

        if (singleplayer && collision.gameObject.tag == "AreaE")
        {
            AI = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 currVelocity = rb.velocity;
        // EN SON BURDA TOPUN SEKE SEKE VELOCITY'SININ 0 OLMASINI SAĞLAMAYA ÇALIŞIYORDUN
        // Bunu BallPhysic materyalinden de sağlayabilirsin gibi duruyor
    }

    public void EndGameForPlayers()
    {
        activeGame = false;
    }


}
