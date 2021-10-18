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

    private Stats stats;
    private Rigidbody2D rb;
    private float modifiedSpeed;
    private float modifiedDamage;
    private float modifiedAttackSpeed;

    private bool attacking = false;
    private float timeSpentInAttack = 0f;

    public enum PlayerDirection
    {
        Right,
        Left,
    }
    private void Start()
    {
        stats = this.gameObject.GetComponent<Stats>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        UpdateStats();
    }
    void Update()
    {
        if (GameManager.gameManager.currentScene != GameManager.GameScenes.InGame)
            return;
        if (stats.alive == false)
        { return; }

        UpdateStats();
        if (Input.anyKey)
        {
            CheckInput();
        }
        CheckTimes();
    }
    public void ResetPlayer()
    {
        //if (GameManager.gameManager.currentScene != GameManager.GameScenes.InGame)
          //  return;
        ResetStats();   
        direction = PlayerDirection.Right;
    }
    private void CheckTimes()
    {
        if (GameManager.gameManager.currentScene != GameManager.GameScenes.InGame)
            return;
        if (attacking)
        {
            if(timeSpentInAttack >= lengthOfAttack / modifiedAttackSpeed)
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
        }
    }
    public void UpdateStats()
    {
        stats.ResetMainStats();
        modifiedSpeed = baseSpeed * stats.speedModifier;
        modifiedAttackSpeed = baseSpeed * stats.attackSpeedModifier;
        modifiedDamage = baseDamage * stats.damageModifier;
    }
    public void ResetStats()
    {
        UpdateStats();
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
        } else if (Input.GetKey(KeyCode.D))
        {
            if (direction != PlayerDirection.Right)
                Turn(PlayerDirection.Right);
            Move();
        }

        if (Input.GetKey(KeyCode.L)) // attack
        {
            if (attacking)
                return;
            Attack();
        }
        if (Input.GetKey(KeyCode.W)) // jump
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
    }

    private void Attack()
    {
        if (direction == PlayerDirection.Right)
        {
            attacking = true;
            weapon.transform.Translate(Vector3.right, Space.Self);
        } else
        {
            attacking = true;
            weapon.transform.Translate(Vector3.left, Space.Self);
        }
    }
}
