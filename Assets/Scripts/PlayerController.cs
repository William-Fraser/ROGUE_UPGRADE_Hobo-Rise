using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float baseDamage;
    public float baseAttackSpeed;
    public float lengthOfAttack;
    public float baseSpeed;
    public float baseJumpHeight;
    public PlayerDirection direction;
    public GameObject weapon;
    public Animator animator;
    public Vector2 movementDirection; //Used for Blend Tree
    public AudioSource audioSource;
    public AudioClip footStep;

    private Stats stats;
    private Rigidbody2D rb;
    private float modifiedSpeed;
    private float modifiedDamage;
    private float modifiedAttackSpeed;

    private bool attacking = false;
    private float timeSpentInAttack = 0f;

    private Vector3 previousPosition;

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
        UpdateStats();
    }
    void Update()
    {
        previousPosition = this.transform.position;
        if (GameManager.gameManager.currentScene != GameManager.GameScenes.InGame)
            return;
        if (stats.alive == false)
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
            if (stats.health <= 0 || stats.energy <= 0)
                return;
            CheckInput();
        }
        CheckTimes();
        GameManager.gameManager.DistanceTraveled(Vector3.Distance(previousPosition, this.transform.position));

        if (Input.GetAxis("Horizontal") >= 0.1f || Input.GetAxis("Horizontal") <= -0.1f)
        {
            animator.SetFloat("LastMoveX", Input.GetAxis("Horizontal"));
        }
        Animate();
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
            weapon.GetComponent<Weapon>().attacking = true;
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
            weapon.GetComponent<Weapon>().attacking = false;
        }
    }
    public void UpdateStats()
    {
        modifiedSpeed = baseSpeed * stats.speedModifier;
        modifiedAttackSpeed = baseSpeed * stats.attackSpeedModifier;
        modifiedDamage = baseDamage * stats.damageModifier;
    }
    public void ResetStats()
    {
        UpdateStats();
        stats.ResetMainStats();
        stats.ResetInGameStats();
        weapon.GetComponent<Weapon>().SetDamage(modifiedDamage);
        stats.health = stats.maxHealth;
        if (attacking)
        {
            attacking = false;
            timeSpentInAttack = 0;
            if (direction == PlayerDirection.Right)
                weapon.transform.Translate(Vector3.left, Space.Self);
            else
                weapon.transform.Translate(Vector3.right, Space.Self);
        }
    }
    private void CheckInput()
    {
        if (GameManager.gameManager.currentScene != GameManager.GameScenes.InGame)
            return;
        if (attacking)
            return;
        if (Input.GetKey(KeyCode.A))
        {
            if (direction != PlayerDirection.Left)
                Turn(PlayerDirection.Left);
            Move();
            if (rb.velocity.y != 0 && !rb.IsSleeping())
                return;
            if (audioSource.isPlaying == false)
                audioSource.PlayOneShot(footStep);
        } else if (Input.GetKey(KeyCode.D))
        {
            if (direction != PlayerDirection.Right)
                Turn(PlayerDirection.Right);
            Move();
            if (rb.velocity.y != 0 && !rb.IsSleeping())
                return;
            if (audioSource.isPlaying == false)
                audioSource.PlayOneShot(footStep);
        }

        if (Input.GetKey(KeyCode.L)) // attack
        {
            if (attacking)
                return;
            Attack();
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)) // jump
        {
            if (rb.velocity.y != 0 && !rb.IsSleeping())
                return;
            rb.AddForce(new Vector2(0,baseJumpHeight), ForceMode2D.Impulse);
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
    }
}
