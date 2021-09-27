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

                break;
            case PickupTypes.MysteryMeat:

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
