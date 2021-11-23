using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public GameObject runOverObject;
    public GameObject saveWarning;
    public GameObject bronzeCoinPrefab;
    public GameObject goldCoinPrefab;
    public GameObject billPrefab;
    public PlayerData stats;
    public PlayerData maxPossibleStats;
    public UpgradePrices upgradePrices;
    public GameObject player;
    public bool isNewGame = false;
    public int mainID;
    public int cutsceneID;
    public int inGameID;
    public int victoryID;
    public int resultsID;
    public int upgradeID;
    public int creditsID;
    public GameScenes currentScene;
    public float housePrice = 500;

    public float collectedMoney;

    public AudioSource audioSource;
    public AudioClip buttonPress;

    private float gameOverTimer = 0f;
    private readonly float gameOverTimeRequirement = 3f;

    private bool isKeyDownEndScreen = true;
    [SerializeField]
    private GameObject valuePopupPrefab;

    public enum GameScenes { 
        Main,
        Cutscene,
        InGame,
        Victory,
        GameOver,
        Results,
        Upgrade,
        Credits
    }
    #region Unity Messages
    private void Start()
    {
        if(gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(this.gameObject);
        } else
        {
            Destroy(this.gameObject);
        }
    }
    private void Update()
    {
        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            if(player.GetComponent<Stats>().health <= 0 || player.GetComponent<Stats>().energy <= 0)
            {
                if (currentScene == GameScenes.InGame)
                {
                    EndRound();
                }
            }
        }
    }
    #endregion

    #region Scenes
    public void MainMenu()
    {
        ChangeScene(GameScenes.Main);
        stats = new PlayerData();
    }
    public void NextRound()
    {
        player.SetActive(true);
        player.GetComponent<PlayerController>().ResetPlayer();
        ChangeScene(GameScenes.InGame);
        collectedMoney = 0;
    }
    #endregion

    #region Save and Load System
    public void NewGame()
    {
        AttemptSave();
    }
    public bool IsOnUpgrade()
    {
        if(currentScene == GameScenes.Upgrade)
        {
            return true;
        }
        return false;
    }
    public void Load()
    {
        try
        {
            if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
                PlayerData loadData = (PlayerData)bf.Deserialize(file);
                file.Close();
                stats.maxHealth = loadData.maxHealth;
                stats.speedModifier = loadData.speedModifier;
                stats.damageModifier = loadData.damageModifier;
                stats.attackSpeedModifier = loadData.attackSpeedModifier;
                stats.maxEnergy = loadData.maxEnergy;
                stats.totalMoney = loadData.totalMoney;
                ChangeScene(GameScenes.Upgrade);
            }
        }
        catch
        {
            Debug.LogError("THE LOADED SAVE CANNOT BE PARSED CORRECTLY");
        }
    }
    public void AttemptSave()
    {
        Debug.Log("SAVE ATTEMPT!");
        if (CanLoad())
        {
            saveWarning.SetActive(true);
        } else
        {
            player.GetComponent<PlayerController>().ResetPlayer();
            player.GetComponent<PlayerController>().ResetStats();
            NewGameSetUp();
            ChangeScene(GameScenes.Cutscene);
        }
    }
    public void NewGameSetUp()
    {
        stats.speedModifier = maxPossibleStats.speedModifier / 10;
        stats.damageModifier = maxPossibleStats.damageModifier / 10;
        stats.attackSpeedModifier = maxPossibleStats.attackSpeedModifier / 10;
        stats.maxEnergy = maxPossibleStats.maxEnergy / 10;
        stats.maxHealth = maxPossibleStats.maxHealth / 10;
    }
    public bool CanLoad()
    {
        bool canLoad = false;

        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            canLoad = true;
        }

        return canLoad;
    }
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData saveData = new PlayerData
        {
            attackSpeedModifier = stats.attackSpeedModifier,
            damageModifier = stats.damageModifier,
            maxEnergy = stats.maxEnergy,
            maxHealth = stats.maxHealth,
            speedModifier = stats.speedModifier,
            totalMoney = stats.totalMoney
        };
        bf.Serialize(file, saveData);
        file.Close();
    }
    #endregion

    #region Change Scene Methods
    public void ChangeScene(GameScenes targetScene)
    {
        Save();
        currentScene = targetScene;
        switch (currentScene) {
            case GameScenes.Main:
                SceneManager.LoadScene(mainID);
                break;
            case GameScenes.Cutscene:
                SceneManager.LoadScene(cutsceneID);
                break;
            case GameScenes.InGame:
                SceneManager.LoadScene(inGameID);
                break;
            case GameScenes.Victory:
                SceneManager.LoadScene(victoryID);
                break;
            case GameScenes.Results:
                SceneManager.LoadScene(resultsID);
                break;
            case GameScenes.Upgrade:
                SceneManager.LoadScene(upgradeID);
                break;
            case GameScenes.Credits:
                SceneManager.LoadScene(creditsID);
                break;
            default:
                Debug.LogError("ERR:Loading Scene");
                SceneManager.LoadScene(upgradeID);
                break;
        }
    }
    #endregion

    #region Stat Upgrades
    public void UpgradeTrigger() // Designed to run everytime you upgrade, just so we could add additional logic without editing each method
    {
        Save();
    }
    public void RemoveMoney(float moneyToRemove)
    {
        stats.totalMoney -= moneyToRemove;
    }
    public bool CanPurchase(int amountToCheckAgainst)
    {
        return stats.totalMoney >= amountToCheckAgainst;
    }
    public void UpgradeHealth()
    {
        stats.maxHealth += maxPossibleStats.maxHealth/10;
        UpgradeTrigger();
    }
    public void UpgradeSpeed()
    {
        stats.speedModifier += maxPossibleStats.speedModifier / 10;
        UpgradeTrigger();
    }
    public void UpgradeDamage()
    {
        stats.damageModifier += maxPossibleStats.damageModifier / 10;
        UpgradeTrigger();
    }
    public void UpgradeAttackSpeed()
    {
        stats.attackSpeedModifier += maxPossibleStats.attackSpeedModifier / 10;
        UpgradeTrigger();
    }
    public void UpgradeEnergy()
    {
        stats.maxEnergy += maxPossibleStats.maxEnergy / 10;
        UpgradeTrigger();
    }
    #endregion

    #region Amount of Upgrades Methods

    public float GetSpeedUpgrades()
    {
        return stats.speedModifier / maxPossibleStats.speedModifier * 100;
    }
    public float GetHealthUpgrades()
    {
        return stats.maxHealth / maxPossibleStats.maxHealth * 100;
    }
    public float GetEnergyUpgrades()
    {
        return stats.maxEnergy / maxPossibleStats.maxEnergy * 100;
    }
    public float GetAttackUpgrades()
    {
        return stats.damageModifier / maxPossibleStats.damageModifier * 100;
    }
    public float GetAttackSpeedUpgrades()
    {
        return stats.attackSpeedModifier / maxPossibleStats.attackSpeedModifier * 100;
    }
    #endregion

    #region Housing Methods
    public void BuyHouse()
    {
        ChangeScene(GameScenes.Victory);
    }
    #endregion

   
    #region Money Methods
    public void CollectMoney(int value, Vector3 pos)
    {
        collectedMoney += value;
        gameManager.DisplayGUIPopup("+$"+value, pos, Color.yellow);
    }

    public void ButtonPressed()
    {
        audioSource.loop = false;
        audioSource.PlayOneShot(buttonPress);
    }
    public void SpawnBronzeCoin(Vector3 position)
    {
        GameObject coin = GameObject.Instantiate(bronzeCoinPrefab);
        coin.transform.position = position;
    }
    public void SpawnGoldCoin(Vector3 position)
    {
        GameObject coin = GameObject.Instantiate(goldCoinPrefab);
        coin.transform.position = position;
    }
    public void SpawnBill(Vector3 position)
    {
        GameObject bill = GameObject.Instantiate(billPrefab);
        bill.transform.position = position;
    }
    public float GetMoney()
    {
        return stats.totalMoney;
    }
    public float GetHousePrice()
    {
        return housePrice;
    }
    #endregion

    private bool CheckRoundEnd()
    {
        if (player != null && currentScene == GameScenes.InGame)
        {
            if (player.GetComponent<Stats>().health <= 0 || player.GetComponent<Stats>().energy <= 0)
            {
                return true;
            }
        }
        return false;
    }
    private void EndRound()
    {
        if(isKeyDownEndScreen && !Input.anyKey)
        {
            isKeyDownEndScreen = false;
        }

        if (gameOverTimer >= gameOverTimeRequirement || (isKeyDownEndScreen == false && Input.anyKey))
        {
            runOverObject.SetActive(false);
            gameOverTimer = 0;
            stats.totalMoney += collectedMoney;
            ChangeScene(GameScenes.Upgrade);
            isKeyDownEndScreen = true;
        }
        else
        {
            runOverObject.SetActive(true);
            gameOverTimer += Time.deltaTime;
        }
    }

    public void DisplayGUIPopup(string displayValue, Vector3 pos, Color color)
    {
        //Debug.Log($"GM DISPLAYGUIPOP value: {displayValue}, pos: {pos}, colour: {color}");
        GameObject popup = Instantiate(valuePopupPrefab, pos, Quaternion.identity);
        popup.GetComponent<ValuePopup>().Setup(displayValue, color);
    }
}

[Serializable]
public class PlayerData
{
    public float maxHealth = 10;
    public float speedModifier = 1;
    public float damageModifier = 1;
    public float attackSpeedModifier = 1;
    public float maxEnergy = 10;
    public float totalMoney = 0;
}
[Serializable]
public class UpgradePrices {
    public int[] health;
    public int[] speed;
    public int[] damage;
    public int[] attackSpeed;
    public int[] energy;

    public int GetHealthPrice(float playerStat, float maxStat)
    {
        int i = (int)((playerStat / maxStat * 100) / 10);
        return health[i - 1];
    }
    public int GetSpeedPrice(float playerStat, float maxStat)
    {
        int i = (int)((playerStat / maxStat * 100) / 10);
        return speed[i - 1];
    }
    public int GetDamagePrice(float playerStat, float maxStat)
    {
        int i = (int)((playerStat / maxStat * 100) / 10);
        return damage[i - 1];
    }
    public int GetAttackSpeedPrice(float playerStat, float maxStat)
    {
        int i = (int)((playerStat / maxStat * 100) / 10);
        return attackSpeed[i - 1];
    }
    public int GetEnergyPrice(float playerStat, float maxStat)
    {
        int i = (int)((playerStat / maxStat * 100) / 10);
        return energy[i-1];
    }
}

