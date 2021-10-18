using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartLocation : MonoBehaviour
{
    private void Awake()
    {
        GameManager.gameManager.player.transform.position = this.transform.position;
    }
}
