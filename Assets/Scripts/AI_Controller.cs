using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

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
    public GameObject targetingView;

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

    [Header("Chase")]
    [Tooltip(" Amount of time before AI stops chasing target ")]
    public int chaseCountdown = 5;

    [Header("Attack")]
    public float attackRange = 3f;
    public float attackSpeed = 2f;
    public float attackCoolDown = 3f;
    public int attackDamage = 10;

    [Header("Search")]
    public int searchTime = 10;

    //private fields
    private GameObject targetCheckObject;
    public STATE state;
    private Stats stats;
    private GameObject targetObject;
    private AIDestinationSetter destination;
    //patrol
    private GameObject patrolToPoint; // this field tells the other patrol points what to do
    private GameObject returnPoint;
    //pathfinding
    private Path path;
    private int currentWaypoint = 0;
    private bool isGrounded = false;
    private Seeker seeker;
    private Rigidbody2D rb;
    private float defaultTriggerDistance;
    //chasing
    private bool targetVisible;
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
    //search
    private GameObject searchThis;
    private GameObject searchPoint1;
    private GameObject searchPoint2;
    private GameObject searchPoint3;

    public bool TargetVisible { set { targetVisible = value; } }

    #region Unity Messages
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponentInChildren<Stats>();
        destination = GetComponent<AIDestinationSetter>();
        returnPoint = new GameObject();
        returnPoint.transform.position = transform.position;

        canAttack = true;
        followEnabled = true;
        state = STATE.IDLE;
        targetObject = target.gameObject;
        defaultTriggerDistance = triggerDistance;

        searchThis = new GameObject();
        searchPoint1 = new GameObject();
        searchPoint2 = new GameObject();
        searchPoint3 = new GameObject();

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
                    // state reserved for chasing functionality
                }
                break;

            case STATE.ATTACK:
                {
                    /// stops upon reaching character and try's attacking
                    if (canAttack)
                    { 
                        canAttack = false;
                        Debug.Log("Enemy Attacking");
                        if (target.gameObject.GetComponent<Stats>() != null)
                            target.gameObject.GetComponent<Stats>().LoseHealth(attackDamage);
                        canAttackTimer = attackCoolDown;
                        canAttackCountDown = true;
                    }
                }
                break;

            case STATE.SEARCH:
                {
                    /// sets up temp patrol points around last seen area and searches them
                    targetObject = searchThis;
                    if (searchThis == searchPoint1)
                    {
                        if ((transform.position.x - searchPoint1.transform.position.x) < 2)
                        { searchThis = searchPoint2; }
                    }
                    else if (searchThis == searchPoint1)
                    {
                        if ((transform.position.x - searchPoint2.transform.position.x) < 2)
                        { searchThis = searchPoint3; }
                    }
                    else if (searchThis == searchPoint1)
                    {
                        if ((transform.position.x - searchPoint3.transform.position.x) < 2)
                        { state = STATE.RETURN; targetObject = returnPoint; }
                    }
                }
                break;

            case STATE.RETURN:
                {
                    /// returns to point left during patrol 
                    targetObject = returnPoint;
                    if (Vector2.Distance(transform.position, returnPoint.transform.position) < 7)
                    {
                        state = STATE.PATROL;
                    }
                }
                break;
        }
    }
#endregion

    #region Astar Messages
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
                targetingView.transform.localPosition = new Vector3(-0.3f, 0, 0);
            }
            else if (rb.velocity.x < -0.05f)
            {
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                targetingView.transform.localPosition = new Vector3(0.3f, 0, 0);
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
    #endregion

    #region public methods
    public void ViewTriggerEnter(Collider2D collision) // CHASE STATE
    {
        for (int i = 0; i < tagWhitelist.Length; i++)
        {
            if (tagWhitelist[i].Tag.Contains(collision.tag))
            {
                returnPoint.transform.position = new Vector3(transform.position.x, 0, 0);
                state = STATE.CHASE;
                targetVisible = true;
                targetObject = collision.gameObject;

                triggerDistance = defaultTriggerDistance; // change trigger distance back for chasing
            }
        }
    }
    public void ViewTriggerStay(Collider2D collision)
    {
        // enters attack range
        if (Vector2.Distance(this.transform.position, target.position) < attackRange)
        {
            //Debug.Log("Collision");
            if (collision.gameObject.GetComponent<Stats>() != null)
            {
                state = STATE.ATTACK;
            }
        }
    }
    public void ViewTriggerExit()
    {
        targetVisible = false;
        StartCoroutine("ChaseCountdown");
    }
    public void SetSearchArea()
    {
        if (rb.velocity.x > 0.05f)
        {
            searchPoint1.transform.position = target.position + Vector3.right;
            searchPoint2.transform.position = target.position - Vector3.right;
            searchPoint3 = searchPoint1;
        }
        else if (rb.velocity.x < -0.05f)
        {
            searchPoint1.transform.position = target.position - Vector3.right;
            searchPoint2.transform.position = target.position + Vector3.right;
            searchPoint3 = searchPoint1;
        }
        searchThis = searchPoint1;
    }
    #endregion

    #region Coroutines
    IEnumerator AttackingTimer()
    {
        yield return new WaitForSeconds(attackSpeed);
        attacking = false;
    }
    IEnumerator CanAttackTimer()
    {
        yield return new WaitForSeconds(attackCoolDown);
        canAttack = true;
    }
    IEnumerator ChaseCountdown()
    {
        yield return new WaitForSeconds(chaseCountdown);
        if (targetVisible)
            StopCoroutine("ChaseCountdown");
        SetSearchArea();
        state = STATE.SEARCH;
        StartCoroutine(SearchTime());
    }
    IEnumerator SearchTime()
    {
        yield return new WaitForSeconds(searchTime);
        if (state == STATE.SEARCH)
            state = STATE.RETURN;
    }
    #endregion
}
[System.Serializable]
public class TagWhitelist
{
    public string Tag;
}
