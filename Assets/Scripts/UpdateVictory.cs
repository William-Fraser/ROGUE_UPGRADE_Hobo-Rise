using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateVictory : MonoBehaviour
{
    public GameObject shack;
    public GameObject house;
    public GameObject mansion;

    private void Update()
    {
        switch (GameManager.gameManager.houseBought) {
            case GameManager.HouseBought.Shack:
                shack.SetActive(true);
                house.SetActive(false);
                mansion.SetActive(false);
                break;
            case GameManager.HouseBought.House:
                shack.SetActive(false);
                house.SetActive(true);
                mansion.SetActive(false);
                break;
            case GameManager.HouseBought.Mansion:
                shack.SetActive(false);
                house.SetActive(false);
                mansion.SetActive(true);
                break;
        }
    }
}
