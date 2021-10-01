using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public PickupTypes type;
    public int effectValue;
    public GameObject player;
    public enum PickupTypes { 
        Coin,
        Bill,
        Food,
        MysteryMeat
    }
    private void Collected()
    {
        switch (type) {
            case PickupTypes.Coin:
                player.GetComponent<PlayerStats>().ObtainCoins(effectValue);
                break;
            case PickupTypes.Bill:
                player.GetComponent<PlayerStats>().ObtainCoins(effectValue);
                break;
            case PickupTypes.Food:
                player.GetComponent<PlayerStats>().ObtainEnergy(effectValue);
                break;
            case PickupTypes.MysteryMeat:
                RandomizeEffect();
                break;
        }

        this.gameObject.SetActive(false);
    }
    private void RandomizeEffect()
    {
        switch (Random.Range(0, 2)) {
            case 0:
                player.GetComponent<PlayerStats>().ObtainEnergy(effectValue);
                break;
            case 1:
                player.GetComponent<PlayerStats>().ObtainHealth(effectValue);
                break;
            case 2:
                player.GetComponent<PlayerStats>().LoseHealth(effectValue);
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            Collected();
        }
    }
}
