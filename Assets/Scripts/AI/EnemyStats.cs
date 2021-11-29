using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public GameObject topParent;
    public bool alive = true;
    public float health;
    public CharacterType type;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip takeDamageSound;
    public AudioClip deathSound;
    public enum CharacterType
    {
        Hobo,
        TaxCollector,
        Police
    }
    private void Update()
    {
        if (health <= 0)
        {
            Death();
        }
    }
    public void LoseHealth(int value)
    {
        if (value < 0)
        {
            Debug.LogWarning("Damage is: " + value);
            value = 1;
        }
        if (health - value < 0)
        {
            audioSource.PlayOneShot(takeDamageSound);
            health = 0;
        }
        else
        {
            health -= value;
        }
        GameManager.gameManager.DisplayGUIPopup("-" + value, this.transform.position, Color.red);

        if (health < 0)
            health = 0;
    }

    private void Death()
    {
        alive = false;
        //calculating drop pos / positioning from world scale
        float itemDropPosY = transform.position.y - (transform.localPosition.y + transform.localPosition.y / 2);
        Vector3 itemDropPos = new Vector3(transform.position.x, itemDropPosY, transform.position.z);

        audioSource.PlayOneShot(deathSound);

        switch (type)
        {
            case CharacterType.Hobo:
                CoinManager.coinManager.SpawnBronzeCoin(itemDropPos);
                topParent.SetActive(false);
                break;
            case CharacterType.TaxCollector:
                CoinManager.coinManager.SpawnGoldCoin(itemDropPos);
                topParent.SetActive(false);
                break;
            case CharacterType.Police:
                CoinManager.coinManager.SpawnBill(itemDropPos);
                topParent.SetActive(false);
                break;
        }
        
    }
}