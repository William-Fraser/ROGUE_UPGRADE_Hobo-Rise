using UnityEngine;

public class SaveWarningControl : MonoBehaviour
{
    public void ForceSave()
    {
        GameManager.gameManager.ButtonPressed();
        GameManager.gameManager.player.GetComponent<PlayerController>().ResetPlayer();
        GameManager.gameManager.saveWarning.SetActive(false);
        GameManager.gameManager.ChangeScene(GameManager.GameScenes.Cutscene);
    }
    public void StopSave()
    {
        GameManager.gameManager.ButtonPressed();
        GameManager.gameManager.saveWarning.SetActive(false);
    }
}
