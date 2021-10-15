using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMachine : MonoBehaviour
{
    [Range(1, 15)]
    public float zoom = 1;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] players;
        
        players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            if (player.GetComponent<PlayerController>())
            {
                this.player = player;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = player.transform.position - new Vector3(0, 0, 15-zoom);
    }
}
