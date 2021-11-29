using UnityEngine;

public class SewerHoleTrigger : MonoBehaviour
{
    public GameObject ground;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ground.SetActive(false);
        }
    }
}
