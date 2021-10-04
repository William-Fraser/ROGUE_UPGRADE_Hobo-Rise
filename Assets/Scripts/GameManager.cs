using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    private GameScenes currentScene;

    private enum GameScenes { 
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

    #region Save and Load
    public void Save()
    {

    }
    public void Load()
    {

    }
    #endregion

    #region Change Scene Methods
    private void ChangeScene(GameScenes targetScene)
    {
        currentScene = targetScene;
    }
    #endregion
}
