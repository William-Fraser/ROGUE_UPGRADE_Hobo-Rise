using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public GameObject saveWarning;
    public PlayerData data = new PlayerData();
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
    #endregion

    #region Scenes
    public void MainMenu()
    {
        ChangeScene(GameScenes.Main);
    }
    #endregion

    #region Save and Load System
    public void NewGame()
    {
        isNewGame = true;
        ChangeScene(GameScenes.Upgrade);
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

                data.maxHealth = loadData.maxHealth;
                data.speedModifier = loadData.speedModifier;
                data.damageModifier = loadData.damageModifier;
                data.attackSpeedModifier = loadData.attackSpeedModifier;
                data.maxEnergy = loadData.maxEnergy;
                data.totalMoney = loadData.totalMoney;

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
        if (isNewGame == true && CanLoad())
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
    private void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        data.maxHealth = player.GetComponent<Stats>().maxHealth;
        data.speedModifier = player.GetComponent<Stats>().speedModifier;
        data.damageModifier = player.GetComponent<Stats>().damageModifier;
        data.attackSpeedModifier = player.GetComponent<Stats>().attackSpeedModifier;
        data.maxEnergy = player.GetComponent<Stats>().maxEnergy;
        data.totalMoney = player.GetComponent<Stats>().totalMoney;

        bf.Serialize(file, data);
        file.Close();
    }
    #endregion

    #region Change Scene Methods
    private void ChangeScene(GameScenes targetScene)
    {
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
                SceneManager.LoadScene(upgradeID);
                break;
        }
    }
    #endregion

    #region Stat Upgrades
    public void UpgradeHealth()
    {
        data.maxHealth += 10;
    }
    public void UpgradeSpeed()
    {
        data.speedModifier += 1;
    }
    public void UpgradeDamage()
    {
        data.damageModifier += 1;
    }
    public void UpgradeAttackSpeed()
    {
        data.attackSpeedModifier += 1;
    }
    public void UpgradeEnergy()
    {
        data.maxEnergy += 1;
    }

    #endregion
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
}
