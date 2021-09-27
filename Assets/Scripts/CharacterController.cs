using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed;
    public PlayerDirection direction;

    private PlayerStats stats;

    public enum PlayerDirection
    {
        Right,
        Left,
    }
    private void Start()
    {
        stats = this.gameObject.GetComponent<PlayerStats>();
        UpdateStats();
    }
    void Update()
    {
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
        speed = 1 * stats.speedModifier;
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
    }
    private void Turn(PlayerDirection directionToFace)
    {
        direction = directionToFace;
    }
    private void Move()
    {
        if(direction == PlayerDirection.Left)
        {
            this.transform.Translate(new Vector3(-(speed * Time.deltaTime), 0, 0));
        }
        else if (direction == PlayerDirection.Right)
        {
            this.transform.Translate(new Vector3((speed * Time.deltaTime), 0, 0));
        }
    }
}
