using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    public Transform groundCheckCollider;
    public Transform overheadCheckCollider;
    public LayerMask groundLayer;
    public Transform wallCheckCollider;
    public LayerMask wallLayer;
    const float groundCheckRadius = 0.2f;
    const float overheadCheckRadius = 0.2f;
    const float wallCheckRadius = 0.2f;

    float horizontalValue;
    float runSpeedModifier = 2f;
    public int totalJumps;
    int availableJumps;
    bool facingRight = true;
    bool isRunning;
    //bool jump ;
    bool multipleJump;
    bool coyoteJump;
    bool isSliding;
    bool isDead = false;
    [SerializeField] bool isGrounded;
    //public
    [SerializeField] float speed=2;
    [SerializeField]float jumpPower = 500;
    [SerializeField] float slideFactor = 0.2f;

    // Start is called before the first frame update
    private void Awake()
    {
        availableJumps = totalJumps;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        AudioManager.instance.PlayMusic("nhac");
    }

    // Update is called once per frame
    void Update()
    {
        horizontalValue = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
        }
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
            
        }
        //anim
        anim.SetFloat("yVelocity", rb.velocity.y);
        WallCheck();
    }
    private void FixedUpdate()
    {
        GroundCheck();
        Move(horizontalValue);
    }
    #region Jump
    IEnumerator CoyoteJumpDelay()
    {
        coyoteJump = true;
        yield return new WaitForSeconds(0.2f);
        coyoteJump = false;
    }

    void Jump()
    {
        if (isGrounded)
        {
            multipleJump = true;
            availableJumps--;

            rb.velocity = Vector2.up * jumpPower;
            anim.SetBool("Jump", true);
        }
        else
        {
            if (coyoteJump)
            {
                multipleJump = true;
                availableJumps--;

                rb.velocity = Vector2.up * jumpPower;
                anim.SetBool("Jump", true);
            }

            if (multipleJump && availableJumps > 0)
            {
                availableJumps--;

                rb.velocity = Vector2.up * jumpPower;
                anim.SetBool("Jump", true);
            }
        }
        
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheckCollider.position, groundCheckRadius);
        //Gizmos.color = Color.red;
        //Gizmos.DrawSphere(overheadCheckCollider.position, overheadCheckRadius);
    }
    #endregion
    private void Move(float dir)
    {
        //if (isGrounded&&jumpFlag)
        //{
        //    isGrounded = false;
        //    jumpFlag = false;
        //    rb.AddForce(new Vector2(0f, jumpPower));
        //}
        #region Move&Run
        float xval = dir * speed*100*Time.fixedDeltaTime;
        if (isRunning)
        {
            xval *= runSpeedModifier;
        }
        Vector2 targetVelocity = new Vector2(xval, rb.velocity.y);
        rb.velocity = targetVelocity;
        //lay ti le hien tai
        //lat trai
        if (facingRight && dir < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        }
        else if (!facingRight && dir > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }
        //0 idle,4 walk, 8 run
        anim.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        #endregion
    }
    void GroundCheck()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;
        //Check if the GroundCheckObject is colliding with other
        //2D Colliders that are in the "Ground" Layer
        //If yes (isGrounded true) else (isGrounded false)
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
        {
            isGrounded = true;
            if (!wasGrounded)
            {
                availableJumps = totalJumps;
                multipleJump = false;

                AudioManager.instance.PlaySFX("landing");
            }
            

            //Check if any of the colliders is moving platform
            //Parent it to this transform
            foreach (var c in colliders)
            {
                if (c.tag == "MovingPlatform")
                    transform.parent = c.transform;
            }
        }
        else
        {
            //Un-parent the transform
            transform.parent = null;

            if (wasGrounded)
                StartCoroutine(CoyoteJumpDelay());
            if (groundCheckCollider.position.y < -15f)
            {
                Die();
                AddCoins.instance.setPanel();
            }
        }

        //As long as we are grounded the "Jump" bool
        //in the animator is disabled
        anim.SetBool("Jump", !isGrounded);
    }
    void WallCheck()
    {
        //If we are touching a wall
        //and we are moving towards the wall
        //and we are falling
        //and we are not grounded
        //Slide on the wall
        if (Physics2D.OverlapCircle(wallCheckCollider.position, wallCheckRadius, wallLayer)
            && Mathf.Abs(horizontalValue) > 0
            && rb.velocity.y < 0
            && !isGrounded)
        {
            if (!isSliding)
            {
                availableJumps = totalJumps;
                multipleJump = false;
            }

            Vector2 v = rb.velocity;
            v.y = -slideFactor;
            rb.velocity = v;
            isSliding = true;

            if (Input.GetButtonDown("Jump"))
            {
                availableJumps--;

                rb.velocity = Vector2.up * jumpPower;
                anim.SetBool("Jump", true);
            }
        }
        else
        {
            isSliding = false;
        }
    }
    public void Die()
    {
        isDead = true;
        anim.SetTrigger("Died");
        //FindObjectOfType<LevelManager>().Restart();
    }

    public void ResetPlayer()
    {
        isDead = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "CongChua")
        {
            AddCoins.instance.setWin();
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Save")
        {
            SaveLevel.SavePlayer(this);
            Debug.Log("Save");
        }
        if (collision.gameObject.tag == "Boss")
        {
            FindObjectOfType<HealthBar>().LoseHealth(25);
            Debug.Log("Boss here");
        }
        if (collision.gameObject.tag == "Enemy")
        {
            FindObjectOfType<HealthBar>().LoseHealth(15);
            Debug.Log("Enemy here");
        }
    }
    public void LoadPlayer()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        isDead = false;
        LevelData dt = SaveLevel.Load();
            Vector3 position;
            position.x = dt.position[0];
            position.y = dt.position[1];
            position.z = dt.position[2];
            transform.position = position;
        
    }

}
