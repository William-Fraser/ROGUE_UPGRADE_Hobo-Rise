using UnityEngine;

public class SaveWarningControl : MonoBehaviour
{
    public void ForceSave()
    {
        GameManager.gameManager.isNewGame = false;
        GameManager.gameManager.Save();
        GameManager.gameManager.saveWarning.SetActive(false);
    }
    public void StopSave()
    {
        GameManager.gameManager.saveWarning.SetActive(false);
    }
}
