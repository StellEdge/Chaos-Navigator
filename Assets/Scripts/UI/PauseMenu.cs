using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool pauseState = false;
    private bool pushState = false;

    public GameObject pauseMenu;
    public GameObject endScreen;

    public IntReference endThreshold;

    //public Button exit;
    //public Button restart;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        endScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        PauseListener();
        ShowEndScreen();
        //restart.onClick.AddListener(GameRestart);
        //exit.onClick.AddListener(Application.Quit);
    }

    public void GameRestart()
    {
        Debug.Log("Restarting Game.");
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Resume();
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        pauseState = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        pauseState = false;
    }

    public void QuitGame()
    {
        Debug.Log("Quiting Game.");
        Application.Quit();
    }


    private void PauseListener()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!pauseState)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void ShowEndScreen()
    {
        if(GameObject.Find("BlackHole").GetComponent<BlackHoleController>().absorbNum >= endThreshold)
        {
            endScreen.SetActive(true);
        }

    }

}
