using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AddCoins : MonoBehaviour
{
    private int coins = 0;
    [SerializeField]
    private Text CoinText;
    public static AddCoins instance;
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
    // Start is called before the first frame update
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag=="Player")
    //    {
    //        Destroy(gameObject);
    //        coins =coins+ 10;
    //        if (GameManager.instance != null)
    //        {
    //            GameManager.instance.setScore(coins);
    //        }
    //        AudioManager.instance.PlaySFX("cherry");
    //    }


    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
           
            coins += 10;
            if (GameManager.instance != null&&WinManager.instance!=null)
            {
                GameManager.instance.setScore(coins);
                WinManager.instance.setScore(coins);
            }
            Destroy(collision.gameObject);
            AudioManager.instance.PlaySFX("cherry");
        }

        Debug.Log(coins);
    }

    public void setPanel()
    {
        if (GameManager.instance != null)
        {
            Debug.Log("điểm "+coins);
            GameManager.instance.showpanel(coins);
        }
    }
    public void setWin()
    {
        if (WinManager.instance != null)
        {
            WinManager.instance.showpanel(coins);
        }
    }
}
