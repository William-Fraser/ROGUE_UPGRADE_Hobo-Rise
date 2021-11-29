using UnityEngine;

public class MainMenuUpdate : MonoBehaviour
{
    public GameObject loadButton;

    private void Update()
    {
        if (GameManager.gameManager.CanLoad())
        {
            loadButton.SetActive(true);
        } else
        {
            loadButton.SetActive(false);
        }
    }

}
