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
    public HouseBought houseBought = HouseBought.None;
    public float housePrice = 500;

    public float collectedMoney;
    public int enemiesKilled;
    public float damageDealt;
    public float distanceTraveled;

    public AudioSource audioSource;
    public AudioClip buttonPress;

    private float gameOverTimer = 0f;
    private float gameOverTimeRequirement = 3f;


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
    public enum HouseBought { 
        None,
        Shack,
        Mansion,
        House
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
                    if (gameOverTimer >= gameOverTimeRequirement)
                    {
                        runOverObject.SetActive(false);
                        gameOverTimer = 0;
                        stats.totalMoney += collectedMoney;
                        ChangeScene(GameScenes.Results);
                    } else
                    {
                        runOverObject.SetActive(true);
                        gameOverTimer += Time.deltaTime;
                    }
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

        damageDealt = 0;
        enemiesKilled = 0;
        distanceTraveled = 0;
    }
    #endregion

    #region Save and Load System
    public void NewGame()
    {
        AttemptSave();
    }
    public bool isOnUpgrade()
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
                stats.clout = loadData.clout;
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
            ChangeScene(GameScenes.Cutscene);
        }
    }
    public bool CanLoad()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            return true;
        }
        return false;
    }
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData saveData = new PlayerData();
        saveData.attackSpeedModifier = stats.attackSpeedModifier;
        saveData.damageModifier = stats.damageModifier;
        saveData.maxEnergy = stats.maxEnergy;
        saveData.maxHealth = stats.maxHealth;
        saveData.speedModifier = stats.speedModifier;
        saveData.totalMoney = stats.totalMoney;
        saveData.clout = stats.clout;
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
        AddClout(1);
    }
    public void RemoveMoney(float moneyToRemove)
    {
        stats.totalMoney -= moneyToRemove;
    }
    public void AddClout(int amount)
    {
        stats.clout += amount;
    }
    public bool CheckClout(int amountToCheckAgainst)
    {
        return stats.clout >= amountToCheckAgainst;
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

    #region Housing Methods
    public void BuyHouse(HouseBought typeOfHouse)
    {
        houseBought = typeOfHouse;
        ChangeScene(GameScenes.Victory);
    }
    #endregion

    #region Result Methods
    public void EnemyKilled()
    {
        enemiesKilled += 1;
    }
    public void DamageAdded(float damage, bool isPlayer)
    {
        if(isPlayer)
            damageDealt += damage;
    }

    public void DistanceTraveled(float distance)
    {
        distanceTraveled += distance;
    }
    #endregion
    public void CollectCoins(int value)
    {
        collectedMoney += value;
    }

    public void ButtonPressed()
    {
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
}

[Serializable]
public class PlayerData
{
    public int maxHealth = 10;
    public float speedModifier = 1;
    public float damageModifier = 1;
    public float attackSpeedModifier = 1;
    public int maxEnergy = 10;
    public float totalMoney = 0;
    public int clout = 0;
}
