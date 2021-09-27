using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float baseSpeed;
    public float baseJumpHeight;
    public PlayerDirection direction;

    private PlayerStats stats;
    private Rigidbody2D rb;
    private float modifiedSpeed;
    public enum PlayerDirection
    {
        Right,
        Left,
    }
    private void Start()
    {
        stats = this.gameObject.GetComponent<PlayerStats>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        UpdateStats();
    }
    void Update()
    {
        UpdateStats();
        if (Input.anyKey)
        {
            CheckInput();
        }
    }
    public void Reset()
    {
        UpdateStats();   
        direction = PlayerDirection.Right;
    }
    private void UpdateStats()
    {
        modifiedSpeed = baseSpeed * stats.speedModifier;
    }
    private void CheckInput()
    {
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
        
        
        if (Input.GetKey(KeyCode.Space))
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
}
