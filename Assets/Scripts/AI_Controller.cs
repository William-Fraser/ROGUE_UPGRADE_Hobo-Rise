using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

/// <summary>
/// current err
///     index failure in coroutine (after wait for seconds, unknown source)
///     spacing issues, inspector needs tweaking
///     when killing player, death is ungraceful and removes camera from scene
///     
/// </summary>

public enum STATE
{ 
    IDLE,
    PATROL,
    CHASE,
    ATTACK,
    SEARCH,
    RETURN
}
public class AI_Controller : MonoBehaviour
{
    public TagWhitelist[] tagWhitelist;

    //public fields
    [Header("Pathfinding")]
    //public GameObject playerObject;
    public Transform target;
    public float triggerDistance = 15f;
    [Tooltip("in Seconds, \nHow often AI path updates")]
    public float pathUpdateRate = 1f;

    [Header("Physics")]
    public float speed = 250f;
    [Tooltip("How far away the target needs to be \nbefore AI changes WayPoints")]
    public float nextWaypointDistance = 3f;
    [Tooltip("How high the node needed to jump is \n(lower should cause enemy to jump more while chasing)")]
    public float jumpNodeHeightReqirement = 0.8f;
    public float jumpPower = 0.1f;
    [Tooltip("how high the target node needs to be in order to jump\ncheck to make sure is right")]
    public float jumpCheckOffset = 0.7f;

    [Header("Custom Behaviour")]
    public bool followEnabled = true;
    public bool jumpEnabled = true;
    [Tooltip("allows enemy to change direction \n(useful for animation control)")]
    public bool canTurnAround = true;
    public bool patrolling = false;

    [Header("Patrol Points")] // replace these with auto generating class objects that are contained in a array or list
    public GameObject patrolPoint1;
    public GameObject patrolPoint2;

    [Header("Attack")]
    public float a_Range = 5f;
    public float a_Speed = 2f;
    public float a_CoolDown = 3f;
    public int a_Damage = 10;

    [Space(20)]
    public GameObject weapon;

    //private fields
    private STATE state;
    private Stats stats;
    private GameObject targetObject;
    private AIDestinationSetter destination;
    //patrol
    private GameObject patrolToPoint; // this field tells the other patrol points what to do
    //pathfinding
    private Path path;
    private int currentWaypoint = 0;
    private bool isGrounded = false;
    private Seeker seeker;
    private Rigidbody2D rb;
    private float defaultTriggerDistance;
    private CapsuleCollider2D targetingView;
    //attacking
    private bool canAttack = true;
    private Vector2 direction;
    private bool attacking = false;
    private bool attackLeft;
    private bool attackRight;
    private bool moveWeapon;
    //timer
    private bool attackingCountDown;
    private bool canAttackCountDown;
    private float attackingTimer;
    private float canAttackTimer;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponentInChildren<Stats>();
        destination = GetComponent<AIDestinationSetter>();
        targetingView = GetComponent<CapsuleCollider2D>();

        canAttack = true;
        followEnabled = true;
        state = STATE.CHASE;
        targetObject = target.gameObject;
        defaultTriggerDistance = triggerDistance;

        if (patrolling)
        { state = STATE.PATROL; patrolToPoint = patrolPoint1; targetObject = patrolToPoint; }

        InvokeRepeating("UpdatePath", 0f, pathUpdateRate); // repeats method on loop, checks in seconds on third parameter
    }
    private void FixedUpdate()
    {
        if (!stats.alive)
            return;

        if (attackingCountDown && attackingTimer > 0)
        {
            attackingTimer -= Time.deltaTime;
        }
        else if (attackingCountDown)
        {
            attackingCountDown = false;
            attacking = !attacking;
        }
        
        if (canAttackCountDown && canAttackTimer > 0)
        {
            canAttackTimer -= Time.deltaTime;
        }
        else if (canAttackCountDown)
        {
            canAttackCountDown = false;
            canAttack = !canAttack;
        }

        if (TargetInDistance() && followEnabled)
        {
            PathFollow();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!stats.alive)
            return;

        target = targetObject.transform;

        switch (state)
        {
            case STATE.IDLE:
                {
                    /// face sprite forward?s this.gameObject.GetComponent<CircleCollider2D>().offset = new Vector2(0, 0);
                    /// idles
                    /// play idle anim>?
                    // if patrol type then start timer to start patrolling
                }
                break;

            case STATE.PATROL:
                {
                    targetObject = patrolToPoint;
                    if (triggerDistance != 500)
                    {
                        triggerDistance = 500; // set to patrol format
                    }

                    // starts timer to start Idle // should use scriptable objects for now uses 2 object if statement
                    if (patrolToPoint == patrolPoint1)
                    {
                        if (Vector2.Distance(transform.position, patrolPoint1.transform.position) < 5)
                        { patrolToPoint = patrolPoint2; }
                    }
                    else if (patrolToPoint == patrolPoint2)
                    {
                        if (Vector2.Distance(transform.position, patrolPoint2.transform.position) < 5)
                        { patrolToPoint = patrolPoint1; }
                    }
                }
                break;

            case STATE.CHASE:
                {
                    /// follows characters position
                    /// constantly sets target object transform to target
                    /// 

                    
                }
                break;

            case STATE.ATTACK:
                {
                    /// stops upon reaching character and try's attacking
                    
                }
                break;

            case STATE.SEARCH:
                { 
                    /// sets up temp patrol points around last seen area and searches them
                }
                break;

            case STATE.RETURN:
                { 
                    /// returns to point left during patrol 
                }
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);
        for (int i = 0; i < tagWhitelist.Length; i++)
        {
            if (tagWhitelist[i].Tag.Contains(collision.tag))
            {
                targetObject = collision.gameObject;
                triggerDistance = defaultTriggerDistance; // change trigger distance back for chasing
            }
        }
    }


    private void OnTriggerStay2D(Collider2D collision) // attacks
    {
        // enters attack range
        if (Vector2.Distance(this.transform.position, target.position) < a_Range)
        { 
            //Debug.Log("Collision");
            if (canAttack && collision.gameObject.GetComponent<Stats>() != null)
            {
                canAttack = false;
                Debug.Log("Enemy Attacking");
                if (collision.gameObject.GetComponent<Stats>() != null)
                    collision.gameObject.GetComponent<Stats>().health -= a_Damage;
                canAttackTimer = a_CoolDown;
                canAttackCountDown = true;
            }
        }
    }
    private void UpdatePath()
    {
        if (followEnabled && TargetInDistance() && seeker.IsDone())
        {
            if (targetObject != null)
            { 
                destination.target = targetObject.transform;
            }
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }
    private void PathFollow()
    {
        if (path == null)
        { return; }

        // reach end of path
        if (currentWaypoint >= path.vectorPath.Count)
        { return; }

        // see if colliding with anything 
        Vector2 startOffset = transform.position - new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset);
        isGrounded = Physics2D.Raycast(startOffset, -Vector3.up, 0.05f);

        // Direction calc
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        // Jump
        if (jumpEnabled && isGrounded)
        {
            if (direction.y > jumpNodeHeightReqirement)
            { 
                rb.AddForce(Vector2.up * speed * jumpPower);
            }
        }

        // Movement
        rb.AddForce(force);

        // Next Waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        // Direction Graphics Handling
        if (canTurnAround)
        {
            if (rb.velocity.x > 0.05f)
            {
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                targetingView.offset.Set(3.3f, 0);
            }
            else if (rb.velocity.x < -0.05f)
            {
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                targetingView.offset.Set(-3.3f, 0);
            }
        }
    }
    private bool TargetInDistance()
    {
        // returns true if target is inside trigger distance
        return Vector2.Distance(transform.position, target.transform.position) < triggerDistance;
    }
    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    IEnumerator AttackingTimer()
    {
        yield return new WaitForSeconds(a_Speed);
        attacking = false;
    }
    IEnumerator CanAttackTimer()
    {
        yield return new WaitForSeconds(a_CoolDown);
        canAttack = true;
    }
}
[System.Serializable]
public class TagWhitelist
{
    public string Tag;
}
