using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameInspector : MonoBehaviour
{
    GameObject[] balls = new GameObject[8];
    public Text winnerTxt;
    public GameObject endGamePanel;
    
    // Start is called before the first frame update
    void Start()
    {
        balls = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndGame(string name) 
    {
        endGamePanel.SetActive(true);
        winnerTxt.text = "Winner : " + name;
        Debug.Log("Game finished. Winner : " + name);
        foreach(GameObject ball in balls)
        {
            ball.SendMessage("EndGameForPlayers", SendMessageOptions.DontRequireReceiver);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void MainMenu()
    {
        GameObject menuObject = GameObject.FindGameObjectWithTag("Menu");
        Destroy(menuObject);
        SceneManager.LoadScene("MainMenuScene");
    }
}
