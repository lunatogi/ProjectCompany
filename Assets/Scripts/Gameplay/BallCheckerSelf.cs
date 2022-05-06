using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCheckerSelf : MonoBehaviour
{

    public int ballCounter = 0;
    public string name = "";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(ballCounter == 0)
        {
            Camera.main.SendMessage("EndGame", name, SendMessageOptions.DontRequireReceiver);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            ballCounter++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ballCounter--;
        }
    }
}
