using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.XR.WSA.Input;

public class BallCheckerEnemy : MonoBehaviour
{
    public GameObject[] ballsAI = new GameObject[8];

    public GameObject currentBall;

    public Transform shootingLoc;

    public int level = 2;
    public bool singleplayer = false;
    public int ballCounter = 0;
    public string name = "";

    public float intMoveDelay = 0;
    public float moveDelay = 0;        //Delay for moving current ball to the position
    public float shootDelay = 2f;       //Delay for current ball to shoot (in order to move the ball to shooting position)

    public int ballFinder = 0;
    public float rangeTolerance = 0.006f;

    public bool movingBall = false;
    public bool shootingBall = false;

    public bool lerping = true;         //Stops lerping in the shooting stage
    // Start is called before the first frame update
    void Start()
    {
        GameObject menu = GameObject.FindGameObjectWithTag("Menu");             //Takes the singleplayer datas from the object that comes from Main Menu
        level = menu.gameObject.GetComponent<MainMenuControls>().level;
        singleplayer = menu.gameObject.GetComponent<MainMenuControls>().singleplayer;

        if (level == 1)
        {
            intMoveDelay = 2.0f;
            rangeTolerance = 0.01f;
        }
        else if(level == 2)
        {
            intMoveDelay = 2.0f;
            rangeTolerance = 0.006f;
        }else
        {
            intMoveDelay = 1.0f;
            rangeTolerance = 0.006f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(ballCounter == 0)
        {
            Camera.main.SendMessage("EndGame", name, SendMessageOptions.DontRequireReceiver);
            singleplayer = false;
        }

                                                    
        
                                                    //BELOW IS ONLY ABOUT AI

        

        if (singleplayer)
        {
            moveDelay -= Time.deltaTime;

            if (moveDelay <= 0)
            {
                movingBall = true;
            }
        }


        if (movingBall)                         //Moves current ball to shooting position
        {

            while(currentBall == null)
            {
                if(ballsAI[ballFinder] != null)
                {
                    currentBall = ballsAI[ballFinder];
                }

                ballFinder++;
                if (ballFinder == 8)
                    ballFinder = 0;
            }

            /*
            foreach (GameObject ball in ballsAI)
            {
                if (ball != null)
                {
                    currentBall = ball;
                    break;
                }
            }*/
            


            if (lerping)
                currentBall.transform.position = Vector2.Lerp(currentBall.transform.position, shootingLoc.position, Time.deltaTime * 1.5f);

            shootDelay -= Time.deltaTime;

            if (shootDelay <= 0)
            {
                shootingBall = true;
                //
            }
        }

        if (shootingBall)                   //Shoots the current ball from the position
        {
            print("inside");

            lerping = false;

            Rigidbody2D ballRB = currentBall.GetComponent<Rigidbody2D>();


            Vector2 direction;
            Vector2 intPos;
            Vector2 lastPos;

            intPos = shootingLoc.position;
            lastPos = intPos + new Vector2(0, 0.05f);


            float randomAngle = UnityEngine.Random.Range(-rangeTolerance, rangeTolerance);

            float dirX = intPos.x - lastPos.x;
            dirX += randomAngle;
            float dirY = intPos.y - lastPos.y;
            float dirMag = Mathf.Sqrt((dirX * dirX) + (dirY * dirY));
            dirX /= dirMag;
            dirY /= dirMag;
            direction = new Vector2(dirX * 500, dirY * 500);
            ballRB.AddForce(new Vector2(0, 0) + direction);                   //Force that applying to ball while shooting   

            currentBall = null;

            shootingBall = false;
            movingBall = false;
            lerping = true;
            moveDelay = intMoveDelay;                 //MOVE DELAY DEĞİŞTİĞİNDE BURASI DA DEĞİŞMELİ !!!!!!!!!!
            shootDelay = 2;                 //SHOOT DELAY DEĞİŞTİĞİNDE BURASI DA DEĞİŞMELİ !!!!!!!!!!
        }

    }


    public void MovingBall()
    {

    }

    public void ShootBall()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            ballCounter++;
        }

        if (singleplayer)
        {
            for(int k = 0; k < 8; k++)
            {
                if(ballsAI[k] == null)
                {
                    ballsAI[k] = collision.gameObject;
                    break;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ballCounter--;
        }

        if (singleplayer)
        {
            for (int k = 0; k < 8; k++)
            {
                if (ballsAI[k] == collision.gameObject)
                {
                    ballsAI[k] = null;
                    break;
                }
            }
        }
    }
}
