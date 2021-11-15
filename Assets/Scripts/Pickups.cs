using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public PickupTypes type;
    public int effectValue;
    
    private GameObject player;
    
    public enum PickupTypes { 
        Coin,
        Bill,
        Food
    }
    private void Awake()
    {
        player = GameManager.gameManager.player;
    }
    private void Collected()
    {
        switch (type) {
            case PickupTypes.Coin:
                GameManager.gameManager.CollectCoins(effectValue);
                break;
            case PickupTypes.Bill:
                GameManager.gameManager.CollectCoins(effectValue);
                break;
            case PickupTypes.Food:
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
        }
    }
}
