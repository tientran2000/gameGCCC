using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedScene : MonoBehaviour
{
    public static bool gameIsPause = false;
    public GameObject pauseMenuUI;
    // Update is called once per frame
    public void SetPanel()
    {
        
            if (gameIsPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPause = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPause = true;
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Start");
    }
    public void quitGame()
    {
        Debug.Log("quit Game .....");
        Application.Quit();
    }
}
