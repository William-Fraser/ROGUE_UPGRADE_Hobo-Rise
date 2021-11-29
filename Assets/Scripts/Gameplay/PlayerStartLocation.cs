using UnityEngine;

public class PlayerStartLocation : MonoBehaviour
{
    private void Awake()
    {
        GameManager.gameManager.player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GameManager.gameManager.player.transform.position = this.transform.position;
    }
}
