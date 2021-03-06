using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    public Text coinText;
    public bool inGame;


    private void OnEnable()
    {
        if(GameManager.gameManager.currentScene == GameManager.GameScenes.InGame)
        {
            inGame = true;
        } else
        {
            inGame = false;
        }
    }
    private void Update()
    {
        if (inGame)
        {
            coinText.text = "Coins: " + MoneyManager.moneyManager.GetCollectedMoney();
        } else
        {
            coinText.text = "Coins: " + MoneyManager.moneyManager.GetMoney();
        }
    }
}
