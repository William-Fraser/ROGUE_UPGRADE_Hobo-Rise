using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownHall : MonoBehaviour
{
    public GameObject townHallCanvas;
    public Text shackText;
    public Text houseText;
    public Text mansionText;
    private bool canvasOpen;
    private bool isPlayerHere;
    private int shackPrice = 50;
    private int housePrice = 150;
    private int mansionPrice = 250;
    private void Update()
    {
        Debug.LogWarning(Time.timeScale);
        if(isPlayerHere && Input.GetKeyDown(KeyCode.E))
        {
            Time.timeScale = 0;
            CanvasSetActive(true);
        }

        if (canvasOpen)
        {
            if (GameManager.gameManager.CanPurchase(shackPrice))
            {
                shackText.text = "Buy: " + shackPrice;
            } else
            {
                shackText.text = "Cannot Afford: $" + shackPrice;
            }

            if (GameManager.gameManager.CanPurchase(housePrice))
            {
                houseText.text = "Buy: " + housePrice;
            }
            else
            {
                houseText.text = "Cannot Afford: $" + housePrice;
            }

            if (GameManager.gameManager.CanPurchase(mansionPrice))
            {
                mansionText.text = "Buy: " + mansionPrice;
            }
            else
            {
                mansionText.text = "Cannot Afford: $" + mansionPrice;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == GameManager.gameManager.player)
        {
            if (GameManager.gameManager.CheckClout(10))
                isPlayerHere = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerHere = false;
    }

    public void CloseMenu()
    {
        CanvasSetActive(false);
        Time.timeScale = 1;
    }

    public void PurchaseShack()
    {
        if (GameManager.gameManager.CanPurchase(shackPrice))
        {
            GameManager.gameManager.BuyHouse(GameManager.HouseBought.Shack);
        }
    }
    public void PurchaseHouse()
    {
        if (GameManager.gameManager.CanPurchase(housePrice))
        {
            GameManager.gameManager.BuyHouse(GameManager.HouseBought.House);
        }
    }
    public void PurchaseMansion()
    {
        if (GameManager.gameManager.CanPurchase(mansionPrice))
        {
            GameManager.gameManager.BuyHouse(GameManager.HouseBought.Mansion);
        }
    }

    private void CanvasSetActive(bool active)
    {
        townHallCanvas.SetActive(active);
        canvasOpen = active;
    }
}
