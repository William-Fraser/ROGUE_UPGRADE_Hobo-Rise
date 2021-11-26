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

        PlayerData stats = GameManager.gameManager.GetPlayerStats();
        PlayerData maxStats = GameManager.gameManager.GetMaxStats();

        switch (statType)
        {
            case ButtonType.ATKSPD:
                if (GameManager.gameManager.CanPurchase(prices.GetAttackSpeedPrice(stats.attackSpeedModifier, maxStats.attackSpeedModifier)) && stats.attackSpeedModifier != maxStats.attackSpeedModifier)
                {
                    button.Enable();
                }
                else button.Disable();
                break;
            case ButtonType.SPD:
                if (GameManager.gameManager.CanPurchase(prices.GetSpeedPrice(stats.speedModifier, maxStats.speedModifier)) && stats.speedModifier != maxStats.speedModifier)
                {
                    button.Enable();
                }
                else button.Disable();
                break;
            case ButtonType.DMG:
                if (GameManager.gameManager.CanPurchase(prices.GetDamagePrice(stats.damageModifier, maxStats.damageModifier)) && stats.damageModifier != maxStats.damageModifier)
                {
                    button.Enable();
                }
                else button.Disable();
                break;
            case ButtonType.HP:
                if (GameManager.gameManager.CanPurchase(prices.GetHealthPrice(stats.maxHealth, maxStats.maxHealth)) && stats.maxHealth != maxStats.maxHealth)
                {
                    button.Enable();
                }
                else button.Disable();
                break;
            case ButtonType.NRG:
                if (GameManager.gameManager.CanPurchase(prices.GetEnergyPrice(stats.maxEnergy, maxStats.maxEnergy)) && stats.maxEnergy != maxStats.maxEnergy)
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
