using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControls : MonoBehaviour
{
    public GameObject levelPanel;
    public GameObject mainPanel;

    public int level = 1;
    public bool singleplayer = true;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Singleplayer()
    {
        singleplayer = true;
        levelPanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void Multiplayer()
    {
        singleplayer = false;
        SceneManager.LoadScene("GameScene");
    }

    public void LevelSelect(int a)
    {
        level = a + 1;
        SceneManager.LoadScene("GameScene");
    }

    public void Online()
    {

    }

    public void SingleExit()
    {
        singleplayer = false;
        levelPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
}
