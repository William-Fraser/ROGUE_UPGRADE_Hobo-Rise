using UnityEngine;

public class SaveWarningControl : MonoBehaviour
{
    public void ForceSave()
    {
        GameManager.gameManager.player.GetComponent<PlayerController>().ResetPlayer();
        GameManager.gameManager.saveWarning.SetActive(false);
        GameManager.gameManager.ChangeScene(GameManager.GameScenes.InGame);
    }
    public void StopSave()
    {
        GameManager.gameManager.saveWarning.SetActive(false);
    }
}
