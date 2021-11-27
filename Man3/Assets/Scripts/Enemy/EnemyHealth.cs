using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public Animator anim;
    //public HealthBar health;
    public static EnemyHealth instance;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
       // health.SetHealth(currentHealth, maxHealth);
    }

    // Update is called once per frame
    private void Awake()
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
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("hurt");
        Debug.Log(currentHealth);
       // health.SetHealth(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
           // Die();
            anim.SetBool("dead", true);
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
            GetComponent<EnemyAI>().enabled = false;
        }
    }
    public void Die()
    {
        Debug.Log("quái đã chết");
        anim.SetBool("dead", true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        GetComponent<EnemyAI>().enabled = false;
       // Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            anim.SetTrigger("attack");
            //anim.SetTrigger("attackfire");
        }
    }
}
