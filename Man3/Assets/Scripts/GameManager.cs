using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Text scoretext;
    public static GameManager instance;
    [SerializeField]
    private Text endscore;
    [SerializeField]
    private GameObject panel;
    void Awake()
    {
        makeInstance();
    }

    void makeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void setScore(int score)
    {
        scoretext.text = "" + score;
    }

    public void showpanel(int score)
    {
        panel.SetActive(true);
        endscore.text = "" + score;

    }
    public void reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
