using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTitle : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool hit;
    private float direction;
    private Animator anim;
    private BoxCollider2D boxCollider;
   // private EnemyHealth enemy;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime*direction;
        transform.Translate(movementSpeed, 0, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");
        
            EnemyHealth.instance.TakeDamage(20);
        
        
    }
    public void SetDirection(float d)
    {
        direction = d;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float local = transform.localScale.x;
        if(Mathf.Sign(local) != d)
        {
            local = -local;
        }
        transform.localScale = new Vector3(local, transform.localScale.y, transform.localScale.z);
    }
    private void DeActivate()
    {
        gameObject.SetActive(false);
    }
}
