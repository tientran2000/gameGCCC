using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //public Slider slider;
    //public Color low;
    //public Color high;
    //public Vector3 offset;
    //public void SetHealth(float health, float maxhealth)
    //{
    //    slider.gameObject.SetActive(health < maxhealth);
    //    slider.value = health;
    //    slider.maxValue = maxhealth;
    //    slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue);
    //}
    //private void Update()
    //{
    //    slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position = offset);
    //}
    public Image fillBar;
    public float health;
    public void LoseHealth(int value)
    {
        if (health <= 0)
            return;
        health -= value;
        fillBar.fillAmount = health / 100;
        if (health <= 0)
        {
            Debug.Log("You die!ok");
            FindObjectOfType<player>().Die();
            AddCoins.instance.setPanel();
        }
    }
    public void AddHealth()
    {
        //If no lives remaining do nothing
        if (health == 100)
            return;
        //Decrease the value of livesRemaining
        health+=25;
        
        fillBar.fillAmount = health / 100;
        //Destroy(gameObject);
        //If we run out of lives we lose the game
        if (health == 0)
        {
            //Debug.Log("you lost");
            //FindObjectOfType<player>().Die();
            AddCoins.instance.setPanel();
        }
    }
}
