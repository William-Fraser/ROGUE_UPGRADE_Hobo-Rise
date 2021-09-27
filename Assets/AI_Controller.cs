using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    //public fields
    public TagWhitelist[] whitelist;

    //private fields
    public GameObject targetObject;
    private STATE state;
    private int patrolTo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case STATE.IDLE:
                {
                    /// idles
                    /// play idle anim>?
                    // if patrol type then start timer to start patrolling
                }
                return;

            case STATE.PATROL:
                { 
                    /// moves between patrol points
                    
                    // starts timer to start Idle
                }
                return;

            case STATE.CHASE:
                {
                    /// follows characters position
                }
                return;

            case STATE.ATTACK:
                { 
                    /// stops before reaching character and try's attacking
                }
                return;

            case STATE.SEARCH:
                { 
                    /// sets up temp patrol points around last seen area and searches them
                }
                return;

            case STATE.RETURN:
                { 
                    /// returns to point left during patrol 
                }
                return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < whitelist.Length; i++)
        { 
            if (whitelist[i].Tag.Contains(collision.tag))
            {
                targetObject = collision.gameObject;
            }
        }
    }
}
[System.Serializable]
public class TagWhitelist
{
    public string Tag;
}
