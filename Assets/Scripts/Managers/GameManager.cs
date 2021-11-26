using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public GameScenes currentScene;
    public GameObject player;
    public AudioSource audioSource;
    public AudioClip buttonPress;

    [SerializeField]
    private GameObject runOverObject;
    [SerializeField]
    private PlayerData maxPossibleStats;
    [SerializeField]
    private UpgradePrices upgradePrices;
    [SerializeField]
    private int mainID;
    [SerializeField]
    private int cutsceneID;
    [SerializeField]
    private int inGameID;
    [SerializeField]
    private int victoryID;
    [SerializeField]
    private int upgradeID;
    [SerializeField]
    private int creditsID;
    [SerializeField]
    private float housePrice = 500;
    [SerializeField]
    private GameObject valuePopupPrefab;

    private PlayerData stats;
    private float collectedMoney;
    private float gameOverTimer = 0f;
    private readonly float gameOverTimeRequirement = 3f;
    private bool isKeyDownEndScreen = true;

    public enum GameScenes
    {
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
        if (gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Update()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        if (CheckRoundEnd())
        {
            EndRound();
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
        if (currentScene == GameScenes.Upgrade)
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
        if (CanLoad())
        {
            gameObject.GetComponent<SaveWarningControl>().ActivateWarning();
        }
        else
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
        switch (currentScene)
        {
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

    public void UpgradeHealth()
    {
        stats.maxHealth += maxPossibleStats.maxHealth / 10;
        Save();
    }
    public void UpgradeSpeed()
    {
        stats.speedModifier += maxPossibleStats.speedModifier / 10;
        Save();
    }
    public void UpgradeDamage()
    {
        stats.damageModifier += maxPossibleStats.damageModifier / 10;
        Save();
    }
    public void UpgradeAttackSpeed()
    {
        stats.attackSpeedModifier += maxPossibleStats.attackSpeedModifier / 10;
        Save();
    }
    public void UpgradeEnergy()
    {
        stats.maxEnergy += maxPossibleStats.maxEnergy / 10;
        Save();
    }


    #endregion

    #region Housing Methods
    public void BuyHouse()
    {
        ChangeScene(GameScenes.Victory);
    }
    #endregion

    #region Money Methods
    public UpgradePrices GetUpgradePrices()
    {
        return upgradePrices;
    }
    public void RemoveMoney(float moneyToRemove)
    {
        stats.totalMoney -= moneyToRemove;
    }
    public bool CanPurchase(int amountToCheckAgainst)
    {
        return stats.totalMoney >= amountToCheckAgainst;
    }
    public void AddToMoneyTotal(int value)
    {
        stats.totalMoney += value;
    }
    public float GetHousePrice()
    {
        return housePrice;
    }
    #endregion

    #region Misc
    public void ButtonPressed()
    {
        audioSource.loop = false;
        audioSource.PlayOneShot(buttonPress);
    }
    public PlayerData GetMaxStats()
    {
        return maxPossibleStats;
    }
    public PlayerData GetPlayerStats()
    {
        return stats;
    }

    public void DisplayGUIPopup(string displayValue, Vector3 pos, Color color)
    {
        //Debug.Log($"GM DISPLAYGUIPOP value: {displayValue}, pos: {pos}, colour: {color}");
        GameObject popup = Instantiate(valuePopupPrefab, pos, Quaternion.identity);
        popup.GetComponent<ValuePopup>().Setup(displayValue, color);
    }
    #endregion

    #region Round Over
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
        if (isKeyDownEndScreen && !Input.anyKey)
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
    #endregion
}




