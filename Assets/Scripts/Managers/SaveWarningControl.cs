using UnityEngine;

public class SaveWarningControl : MonoBehaviour
{
    [SerializeField]
    private GameObject saveWarning;
    public void ActivateWarning()
    {
        saveWarning.SetActive(true);
    }
    public void ForceSave()
    {
        GameManager.gameManager.ButtonPressed();
        GameManager.gameManager.NewGameSetUp();
        GameManager.gameManager.player.GetComponent<PlayerController>().ResetPlayer();
        saveWarning.SetActive(false);
        GameManager.gameManager.ChangeScene(GameManager.GameScenes.Cutscene);
    }
    public void StopSave()
    {
        GameManager.gameManager.ButtonPressed();
        saveWarning.SetActive(false);
    }
}
