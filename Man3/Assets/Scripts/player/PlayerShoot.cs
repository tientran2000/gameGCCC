using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] public float shootCooldown;
    [SerializeField] public Transform firePoint;
    [SerializeField] public GameObject[] fireball;
    private Animator anim;
    private player p;
    private float cooldownTimer = Mathf.Infinity;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        p = GetComponent<player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)&&cooldownTimer>shootCooldown)
        {
            Shoot();
        }
        cooldownTimer += Time.deltaTime;
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
        for(int i = 0; i < fireball.Length; i++)
        {
            if (!fireball[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
