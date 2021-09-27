using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public PickupTypes type;
    public string playerTag = "Player";
    public GameObject player;
    public int value;
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
                player.GetComponent<PlayerStats>().ObtainCoins(value);
                break;
            case PickupTypes.Bill:
                player.GetComponent<PlayerStats>().ObtainCoins(value);
                break;
            case PickupTypes.Food:
                player.GetComponent<PlayerStats>().ObtainEnergy(value);
                break;
            case PickupTypes.MysteryMeat:
                RandomizeEffect();
                break;
        }
    }
    private void RandomizeEffect()
    {
        switch (Random.Range(0, 2)) {
            case 0:
                player.GetComponent<PlayerStats>().ObtainEnergy(value);
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == playerTag)
        {
            player = other.gameObject;
            Collected();
        }
    }
}
