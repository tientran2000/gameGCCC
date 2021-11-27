using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadlevel : MonoBehaviour
{
    public int iLevelToLoad;
    public string sLevelToLoad;


    public bool useIntegeToLoadLeVel = false; 


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;

        if(collisionGameObject.name == "player")
        {
            LoadScene();
        }
    }
    void LoadScene()
    {
        if (useIntegeToLoadLeVel)
        {
            SceneManager.LoadScene(iLevelToLoad);
        }
        else
        {
            SceneManager.LoadScene(sLevelToLoad); 
        }
    }

}
