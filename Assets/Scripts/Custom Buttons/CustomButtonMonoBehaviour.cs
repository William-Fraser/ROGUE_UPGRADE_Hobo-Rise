using UnityEngine;
public class CustomButtonMonoBehaviour : MonoBehaviour
{
    public ButtonType statType;
    public enum ButtonType { 
        ATKSPD,
        SPD,
        DMG,
        HP,
        NRG,
        House
    }
    private Button button;
    public void SetButton(Button button)
    {
        this.button = button;
    }
    private void Update()
    {
        SetButtonState();
    }
    private void SetButtonState()
    {
        UpgradePrices prices = GameManager.gameManager.GetUpgradePrices();
        if (button == null)
        {
            Debug.Log(gameObject.name);
            this.gameObject.GetComponent<Button>();
        }

        switch (statType)
        {
            case ButtonType.ATKSPD:
                if (GameManager.gameManager.CanPurchase(prices.GetAttackSpeedPrice(GameManager.gameManager.GetAttackSpeedUpgrades())) && GameManager.gameManager.GetAttackSpeedUpgrades() != 100)
                {
                    button.Enable();
                }
                else button.Disable();
                break;
            case ButtonType.SPD:
                if (GameManager.gameManager.CanPurchase(prices.GetSpeedPrice(GameManager.gameManager.GetSpeedUpgrades())) && GameManager.gameManager.GetSpeedUpgrades() != 100)
                {
                    button.Enable();
                }
                else button.Disable();
                break;
            case ButtonType.DMG:
                if (GameManager.gameManager.CanPurchase(prices.GetDamagePrice(GameManager.gameManager.GetAttackUpgrades())) && GameManager.gameManager.GetAttackUpgrades() != 100)
                {
                    button.Enable();
                }
                else button.Disable();
                break;
            case ButtonType.HP:
                if (GameManager.gameManager.CanPurchase(prices.GetHealthPrice(GameManager.gameManager.GetHealthUpgrades())) && GameManager.gameManager.GetHealthUpgrades() != 100)
                {
                    button.Enable();
                }
                else button.Disable();
                break;
            case ButtonType.NRG:
                if (GameManager.gameManager.CanPurchase(prices.GetEnergyPrice(GameManager.gameManager.GetEnergyUpgrades())) && GameManager.gameManager.GetEnergyUpgrades() != 100)
                {
                    button.Enable();
                }
                else button.Disable();
                break;
            case ButtonType.House:
                if (GameManager.gameManager.CanPurchase((int)GameManager.gameManager.GetHousePrice()))
                {
                    button.Enable();
                }
                else button.Disable();
                break;
        }
    }
}
