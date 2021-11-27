using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Winscene : MonoBehaviour
{
    // Update is called once per frame
   
    
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
    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
   
}
