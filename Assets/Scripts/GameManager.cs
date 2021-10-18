using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public GameObject saveWarning;
    public PlayerData stats;
    public PlayerData maxPossibleStats;
    public GameObject player;
    public bool isNewGame = false;
    public int mainID;
    public int inGameID;
    public int victoryID;
    public int gameOverID;
    public int resultsID;
    public int upgradeID;
    public int creditsID;
    public GameScenes currentScene;

    public float collectedMoney;


    public enum GameScenes { 
        Main,
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
            if(player.GetComponent<Stats>().health == 0 || player.GetComponent<Stats>().energy == 0)
            {
                if (currentScene == GameScenes.InGame)
                {
                    stats.totalMoney += collectedMoney; 
                    ChangeScene(GameScenes.Results); 
                }
            }
        }
    }
    #endregion

    #region Scenes
    public void MainMenu()
    {
        ChangeScene(GameScenes.Main);
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
            Save();
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
            case GameScenes.InGame:
                SceneManager.LoadScene(inGameID);
                break;
            case GameScenes.Victory:
                SceneManager.LoadScene(victoryID);
                break;
            case GameScenes.GameOver:
                SceneManager.LoadScene(gameOverID);
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
    public void UpgradeHealth()
    {
        stats.maxHealth += 10;
        UpgradeTrigger();
    }
    public void UpgradeSpeed()
    {
        stats.speedModifier += 1;
        UpgradeTrigger();
    }
    public void UpgradeDamage()
    {
        stats.damageModifier += 1;
        UpgradeTrigger();
    }
    public void UpgradeAttackSpeed()
    {
        stats.attackSpeedModifier += 1;
        UpgradeTrigger();
    }
    public void UpgradeEnergy()
    {
        stats.maxEnergy += 10;
        UpgradeTrigger();
    }
    #endregion

    public void CollectCoins(int value)
    {
        collectedMoney += value;
    }
}

[Serializable]
public class PlayerData
{
    public int maxHealth = 10;
    public float speedModifier = 10;
    public float damageModifier = 1;
    public float attackSpeedModifier = 1;
    public int maxEnergy = 10;
    public float totalMoney = 0;
}
