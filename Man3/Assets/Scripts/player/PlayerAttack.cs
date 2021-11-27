using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator anim;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 0.5f;
    public int attackDamage = 40;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    //
    [SerializeField] public float shootCooldown;
    [SerializeField] public Transform firePoint;
    [SerializeField] public GameObject[] fireball;
    //private Animator anim;
    private player p;
    private float cooldownTimer = Mathf.Infinity;
    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
            if (Input.GetKeyDown(KeyCode.S) && cooldownTimer > shootCooldown)
            {
                Shoot();
            }
            cooldownTimer += Time.deltaTime;
        }


    }
    void Attack()
    {
        anim.SetTrigger("Attack");
        AudioManager.instance.PlaySFX("attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    private void Shoot()
    {
        anim.SetTrigger("Shoot");
        cooldownTimer = 0;
        fireball[FindFireball()].transform.position = firePoint.position;
        fireball[FindFireball()].GetComponent<ProjectTitle>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
    private int FindFireball()
    {
        for (int i = 0; i < fireball.Length; i++)
        {
            if (!fireball[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
