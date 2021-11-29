using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public enum STATE
{ 
    IDLE,
    PATROL,
    CHASE,
    ATTACK
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
    public bool patrolling = true;
    public bool chasing = true;
    public bool hostile = true;

    [Header("Patrol Points")] // replace these with auto generating class objects that are contained in a array or list
    public GameObject patrolPoint1;
    public GameObject patrolPoint2;

    [Header("Attack")]
    public float attackRange = 3f;
    public float attackCoolDown = 3f;
    public int attackDamage = 1;


    //private fields
    public STATE state;
    private EnemyStats stats;
    private GameObject targetObject;
    private AIDestinationSetter destination;
    //patrol
    private GameObject patrolToPoint; // this field tells the other patrol points what to do
    private GameObject returnPoint;
    private float switchToNextPointTimer;
    //pathfinding
    private Path path;
    private int currentWaypoint = 0;
    private bool isGrounded = false;
    private Seeker seeker;
    private Rigidbody2D rb;
    private float defaultTriggerRadius;
    //chasing
    private bool targetVisible;
    //attacking
    private bool canAttack = true;
    private bool shouldAttack = false;
    //timer
    private bool attackingCountDown;
    private float attackingTimer;
    //animation
    private float direction = 0;
    private Animator animator;

    public bool TargetVisible { set { targetVisible = value; } }

    #region Unity Messages
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponentInChildren<EnemyStats>();
        destination = GetComponent<AIDestinationSetter>();
        returnPoint = new GameObject("Return Point");
        returnPoint.transform.position = transform.position;

        canAttack = true;
        state = STATE.IDLE;
        targetObject = target.gameObject;
        defaultTriggerRadius = triggerDistance;


        animator = GetComponentInChildren<Animator>();

        //set up patrol
        patrolToPoint = patrolPoint1; 
        targetObject = patrolToPoint; 

        InvokeRepeating("UpdatePath", 0f, pathUpdateRate); // repeats method on loop, checks in seconds on third parameter
    }
    private void FixedUpdate()
    {
        if (!stats.alive)
            return;

        AttackTimer();
        PatrolPointAutoSwitchTimer();

        // following a path     only if
        if (TargetInDistance() && (hostile || chasing))
        {
            PathFollow();
        }
    }
    void Update()
    {
        if (!stats.alive)
            return;
        
        switch (state)
        {
            case STATE.IDLE:
                {
                    TrySetStatePatrol();
                    TrySetStateChase();
                    TrySetStateAttack();
                }
                break;
            case STATE.PATROL:
                {
                    Patrol();
                    TrySetStateChase();
                }
                break;

            case STATE.CHASE:
                {
                    Chase();
                    TrySetStatePatrol();
                    TrySetStateAttack();
                }
                break;

            case STATE.ATTACK:
                {
                    Attack();
                    TrySetStatePatrol();
                    TrySetStateChase();
                }
                break;       
        }

        Animate();
    }
#endregion

    #region Astar Messages
    private void UpdatePath()
    {
        if (hostile && TargetInDistance() && seeker.IsDone())
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
        if (isGrounded)
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

        // View Direction Handling
        if (rb.velocity.x > 0.001f)
        {
            this.direction = 1f;
            targetingView.transform.localPosition = new Vector3(-0.3f, 0, 0);
        }
        else if (rb.velocity.x < -0.001f)
        {
            this.direction = -1f;
            targetingView.transform.localPosition = new Vector3(0.3f, 0, 0);
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

    #region methods
    
    //private
    private void Patrol()
    {
        // starts timer to start Idle // should use scriptable objects for now uses 2 object if statement
        if (patrolToPoint == patrolPoint1)
        {
            if (Vector3.Distance(transform.position, patrolPoint1.transform.position) < 3)
            { patrolToPoint = patrolPoint2; }
        }
        else if (patrolToPoint == patrolPoint2)
        {
            if (Vector3.Distance(transform.position, patrolPoint2.transform.position) < 3)
            { patrolToPoint = patrolPoint1; }
        }

        target = patrolToPoint.transform;
    }
    private void TrySetStateChase()
    {
        if (targetVisible && !shouldAttack)
        { 
            returnPoint.transform.position = new Vector3(transform.position.x, 0, 0);
            triggerDistance = defaultTriggerRadius; // trigger distance changes depending on chase or patrol, this is the inspector distance
            state = STATE.CHASE;
        }
    }
    private void Chase()
    {
        target = targetObject.transform; // found object
    }
    private void TrySetStateAttack()
    {
        if (hostile && shouldAttack)
        {
            state = STATE.ATTACK;
        }
    }
    private void Attack()
    {
        if (canAttack)
        {
            canAttack = false;
            //Debug.Log("Enemy Attacking");
            
            attackingCountDown = true;
            attackingTimer = attackCoolDown;

            if (target.gameObject.GetComponent<PlayerStats>() != null && target.gameObject.GetComponent<PlayerStats>().GetAlive())
            {
                target.gameObject.GetComponent<PlayerStats>().LoseHealth(attackDamage, true);
            }

        }
    }
    private void TrySetStatePatrol()
    {
        if (!targetVisible)
        {
            triggerDistance = 10000; // set to patrol format
            state = STATE.PATROL;
        }
    }
    private void PatrolPointAutoSwitchTimer()
    {
        if (switchToNextPointTimer > 0)
        { switchToNextPointTimer -= Time.deltaTime; }
        else
        { 
            switchToNextPointTimer = 50;
            if (patrolToPoint == patrolPoint1)
            { patrolToPoint = patrolPoint2; }
            else if (patrolToPoint == patrolPoint2)
            { patrolToPoint = patrolPoint1; }
        }
    }
    private void AttackTimer()// must be in Fixed update
    {
        // set in Attack()
        if (attackingCountDown && attackingTimer > 0)
        {
            attackingTimer -= Time.deltaTime;
        }
        else if (attackingCountDown)
        {
            attackingCountDown = false;
            canAttack = true;
        }
    }
    
    public void Animate()
    {
        if (animator.runtimeAnimatorController)
        {
            animator.SetFloat("Speed", rb.velocity.magnitude);
            animator.SetFloat("LastMoveX", direction);
        }
    }
    //public

    /*IEnumerator CanAttackTimer()
    {
        yield return new WaitForSeconds(attackCoolDown);
        canAttack = true;
    }*/
    #endregion

    #region ViewTriggers 
    // offloaded onto another gameobject for use of sprite in testing
    public void ViewTriggerEnter(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("TARGET FOUND");
            targetObject = collision.gameObject;
            targetVisible = true;
        }
    }
    public void ViewTriggerStay(Collider2D collision)
    {
        // enters attack range
        if (target.tag == "Player")
        {
            if (Vector2.Distance(this.transform.position, target.position) <= attackRange)
            {
                shouldAttack = true;
            }
        }
        else
        {
            shouldAttack = false;
        }
    }
    public void ViewTriggerExit(Collider2D collision)
    {
        if (collision.tag == "Player")
        { 
            targetVisible = false;
        }
    }

    #endregion

}
[System.Serializable]
public class TagWhitelist
{
    public string Tag;
}
