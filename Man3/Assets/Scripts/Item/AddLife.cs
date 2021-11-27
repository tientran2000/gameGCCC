using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class AddLife : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //anim.SetTrigger("attack");
           // FindObjectOfType<LifeCount>().AddLife();
            FindObjectOfType<HealthBar>().AddHealth();
            Destroy(gameObject);
        }
    }
}
