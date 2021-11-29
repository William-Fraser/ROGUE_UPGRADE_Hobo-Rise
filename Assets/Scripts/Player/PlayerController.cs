using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float baseAttackSpeed;
    public float lengthOfAttack;
    public float baseSpeed;
    public float baseJumpHeight;
    public PlayerDirection direction;
    public GameObject weapon;
    public Animator animator;
    public AudioSource audioSource;
    public AudioClip footStepSound;
    public AudioClip wetFootStepSound;
    public AudioClip playerAtkSound;

    private AudioClip activeStepSound; // step sound holder to be player on step
    private Vector2 movementDirection; //Used for Blend Tree
    private Stats stats;
    private Rigidbody2D rb;
    private float modifiedSpeed;
    private float modifiedAttackSpeed;

    private bool jumping = false;
    private bool attacking = false;
    private float timeSpentInAttack = 0f;

    private Vector3 originalWeaponScaling;

    public enum PlayerDirection
    {
        Right,
        Left,
    }
    private void Start()
    {
        stats = this.gameObject.GetComponent<Stats>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        originalWeaponScaling = weapon.transform.localScale;
        movementDirection = new Vector2();
        UpdateStats();
    }
    void Update()
    {
        Animate();
        if (GameManager.gameManager.currentScene != GameManager.GameScenes.InGame && GameManager.gameManager.currentScene != GameManager.GameScenes.GameOver && GameManager.gameManager.currentScene != GameManager.GameScenes.Results)
            return;
        if (stats.GetAlive() == false)
        { return; }

        if (attacking)
        {
            weapon.SetActive(true);
        } else
        {
            weapon.SetActive(false);
        }

        UpdateStats();
        if (Input.anyKey)
        {
            if (stats.GetHealth() <= 0 || stats.GetEnergy() <= 0)
                return;
            CheckInput();
        }
        CheckTimes();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DetectGroundType(collision);
        if (jumping) jumping = false;
    }
    public void ResetPlayer()
    {
        if (GameManager.gameManager.currentScene == GameManager.GameScenes.InGame)
          return;
        ResetStats();   
        direction = PlayerDirection.Right;
    }
    private void CheckTimes()
    {
        if (GameManager.gameManager.currentScene != GameManager.GameScenes.InGame)
            return;
        if (attacking)
        {
            weapon.GetComponent<Weapon>().StartAttack(this.gameObject);
            if (timeSpentInAttack >= lengthOfAttack / modifiedAttackSpeed)
            {
                attacking = false;
                timeSpentInAttack = 0;
                if(direction == PlayerDirection.Right)
                    weapon.transform.Translate(Vector3.left, Space.Self);
                else
                    weapon.transform.Translate(Vector3.right, Space.Self);
            } else
            {
                timeSpentInAttack += Time.deltaTime;
            }
        } else
        {
            weapon.GetComponent<Weapon>().StopAttack();
        }
    }
    public void UpdateStats()
    {
        modifiedSpeed = baseSpeed * stats.GetSpeed();
        modifiedAttackSpeed = baseSpeed * stats.GetAttackSpeed();
    }
    public void ResetStats()
    {
        UpdateStats();
        stats.ResetMainStats();
        stats.ResetInGameStats();
        if (attacking)
        {
            attacking = false;
            timeSpentInAttack = 0;
            if (direction == PlayerDirection.Right)
                weapon.transform.Translate(Vector3.left, Space.Self);
            else
                weapon.transform.Translate(Vector3.right, Space.Self);
        }
        weapon.SetActive(false);
    }
    private void CheckInput()
    {
        if (GameManager.gameManager.currentScene != GameManager.GameScenes.InGame)
            return;
        if (attacking)
            return;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKey(KeyCode.Space)) // check jump before moving
        {
            if (jumping) return;

            jumping = true;
            rb.AddForce(new Vector2(0, baseJumpHeight), ForceMode2D.Impulse);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (direction != PlayerDirection.Left)
                Turn(PlayerDirection.Left);
            Move();
            if (rb.velocity.y != 0 && !rb.IsSleeping())
                return;
            if (audioSource.isPlaying == false)
                audioSource.PlayOneShot(activeStepSound);
        } 
        else if (Input.GetKey(KeyCode.D))
        {
            if (direction != PlayerDirection.Right)
                Turn(PlayerDirection.Right);
            Move();
            if (rb.velocity.y != 0 && !rb.IsSleeping())
                return;
            if (audioSource.isPlaying == false)
                audioSource.PlayOneShot(activeStepSound);
        } else if (Input.GetKey(KeyCode.P))
        {
            stats.LoseEnergy(999);
            Debug.Log("Pass Out Button Pressed");
        }

        if (Input.GetKey(KeyCode.L)) // attack
        {
            if (attacking || jumping)
                return;
            Attack();
        }
        
    }
    private void Turn(PlayerDirection directionToFace)
    {
        direction = directionToFace;
    }
    private void Move()
    {
        if(direction == PlayerDirection.Left)
        {
            this.transform.Translate(new Vector3(-(modifiedSpeed * Time.deltaTime), 0, 0));
        }
        else if (direction == PlayerDirection.Right)
        {
            this.transform.Translate(new Vector3((modifiedSpeed * Time.deltaTime), 0, 0));
        }

        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementDirection.Normalize();
    }

    private void Attack()
    {
        if (direction == PlayerDirection.Right)
        {
            attacking = true;
            weapon.transform.Translate(Vector3.right, Space.Self);
            weapon.transform.localScale = originalWeaponScaling;
        } else
        {
            attacking = true;
            weapon.transform.Translate(Vector3.left, Space.Self);
            weapon.transform.localScale = new Vector3(-originalWeaponScaling.x, originalWeaponScaling.y, originalWeaponScaling.z);
        }
        audioSource.PlayOneShot
            (playerAtkSound);
    }

    private void DetectGroundType(Collision2D collider)
    {
        if (collider.gameObject.tag == "Street")
        { activeStepSound = footStepSound; }
        else if (collider.gameObject.tag == "Sewer")
        { activeStepSound = wetFootStepSound; }
    }

    private void Animate()
    {
        animator.SetFloat("LastMoveX", movementDirection.x);
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && attacking == false)
        {
            if (rb.velocity.y != 0 && !rb.IsSleeping())
                animator.SetFloat("Speed", -1f);
            else
                animator.SetFloat("Speed", 1f);

        }
        else
        {
            animator.SetFloat("Speed", -1f);
        }

        if(direction == PlayerDirection.Right)
        {
            animator.SetBool("MovingRight", true);
        } else
        {
            animator.SetBool("MovingRight", false);
        }
        animator.SetBool("Attacking", attacking);
        
        if(rb.velocity.y > 5 || rb.velocity.y < -5)
        {
            animator.SetBool("Jumping", true);
        } else
        {
            animator.SetBool("Jumping", jumping);
        }
        animator.SetBool("Alive", stats.GetAlive());
    }
}
