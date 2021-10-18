using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_View : MonoBehaviour
{
    public AI_Controller controller;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        controller.CheckChaseTarget(collision);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        controller.TryAttack(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        controller.StartChaseCountdown();
    }
}
