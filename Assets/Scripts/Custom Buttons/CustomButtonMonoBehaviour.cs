using UnityEngine;
public class CustomButtonMonoBehaviour : MonoBehaviour
{
    public Color enabledColor;
    public Color disabledColor;
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

        PlayerData stats = GameManager.gameManager.GetPlayerStats();
        PlayerData maxStats = GameManager.gameManager.GetMaxStats();

        switch (statType)
        {
            case ButtonType.ATKSPD:
                if (MoneyManager.moneyManager.CanPurchase(prices.GetAttackSpeedPrice(stats.attackSpeedModifier, maxStats.attackSpeedModifier)) && stats.attackSpeedModifier != maxStats.attackSpeedModifier)
                {
                    button.Enable(enabledColor);
                }
                else button.Disable(disabledColor);
                break;
            case ButtonType.SPD:
                if (MoneyManager.moneyManager.CanPurchase(prices.GetSpeedPrice(stats.speedModifier, maxStats.speedModifier)) && stats.speedModifier != maxStats.speedModifier)
                {
                    button.Enable(enabledColor);
                }
                else button.Disable(disabledColor);
                break;
            case ButtonType.DMG:
                if (MoneyManager.moneyManager.CanPurchase(prices.GetDamagePrice(stats.damageModifier, maxStats.damageModifier)) && stats.damageModifier != maxStats.damageModifier)
                {
                    button.Enable(enabledColor);
                }
                else button.Disable(disabledColor);
                break;
            case ButtonType.HP:
                if (MoneyManager.moneyManager.CanPurchase(prices.GetHealthPrice(stats.maxHealth, maxStats.maxHealth)) && stats.maxHealth != maxStats.maxHealth)
                {
                    button.Enable(enabledColor);
                }
                else button.Disable(disabledColor);
                break;
            case ButtonType.NRG:
                if (MoneyManager.moneyManager.CanPurchase(prices.GetEnergyPrice(stats.maxEnergy, maxStats.maxEnergy)) && stats.maxEnergy != maxStats.maxEnergy)
                {
                    button.Enable(enabledColor);
                }
                else button.Disable(disabledColor);
                break;
            case ButtonType.House:
                if (MoneyManager.moneyManager.CanPurchase((int)GameManager.gameManager.GetHousePrice()))
                {
                    button.Enable(enabledColor);
                }
                else button.Disable(disabledColor);
                break;
        }
    }
}
