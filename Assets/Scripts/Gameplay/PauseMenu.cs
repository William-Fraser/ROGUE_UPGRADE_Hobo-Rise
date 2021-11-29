using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool paused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
                Pause();
            else
                Unpause();
        }
    }

    private void Pause()
    {
        Time.timeScale = 0;
        paused = true;
        pauseMenu.SetActive(true);
    }
    public void Unpause()
    {
        Time.timeScale = 1;
        paused = false;
        pauseMenu.SetActive(false);
        GameManager.gameManager.ButtonPressed();
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        GameManager.gameManager.ButtonPressed();
        GameManager.gameManager.MainMenu();
    }
}
