using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;

    private void Start()
    {
    }
    private void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y+3, transform.position.z); // magicnumber: to give camera lift so player is along bottom of screen, opposed to the middle
    }
}
