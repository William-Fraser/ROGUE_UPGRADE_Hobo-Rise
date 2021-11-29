using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public PickupTypes type;
    public int effectValue;
    public AudioClip collectCoinSound;
    public AudioClip collectBillSound;
    public AudioClip CollectFoodSound;
    
    private GameObject player;
    private AudioSource audioS;
    
    public enum PickupTypes { 
        Coin,
        Bill,
        Food
    }
    private void Awake()
    {
        player = GameManager.gameManager.player;
        audioS = GameManager.gameManager.audioSource;
    }
    private void Collected()
    {
        switch (type) {
            case PickupTypes.Coin:
                audioS.clip = collectCoinSound;
                MoneyManager.moneyManager.CollectMoney(effectValue, this.transform.position);
                break;
            case PickupTypes.Bill:
                audioS.clip = collectBillSound;
                MoneyManager.moneyManager.CollectMoney(effectValue, this.transform.position);
                break;
            case PickupTypes.Food:
                audioS.clip = CollectFoodSound;
                player.GetComponent<Stats>().ObtainEnergy(effectValue);
                break;
        }

        this.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            Collected();
            audioS.Play();
        }
    }
}
