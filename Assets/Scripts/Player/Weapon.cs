using UnityEngine;

public class Weapon : MonoBehaviour
{
    private bool attacking = false;
    private SpriteRenderer sprite;
    private GameObject attacker;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    public void StartAttack(GameObject attacker)
    {
        this.attacker = attacker;
        attacking = true;
        sprite.sortingOrder = 10;
    }

    public void StopAttack()
    {
        attacking = false;
        sprite.sortingOrder = -1;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!attacking)
            return;

        if (collision.gameObject.GetComponent<EnemyStats>() != null && collision.gameObject != attacker)
        {
            PlayerData stats = GameManager.gameManager.GetPlayerStats();
            collision.gameObject.GetComponent<EnemyStats>().LoseHealth((int)stats.damageModifier);
        }        
    }
}
