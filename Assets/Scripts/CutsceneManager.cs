using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public GameObject[] textBoxObjects;
    public int cutsceneProgress = 0;

    private void Update()
    {
        if(Input.anyKeyDown == true)
        {
            cutsceneProgress += 1;
            GameManager.gameManager.ButtonPressed();
        }
        PlayCutscene();
    }
    public void PlayCutscene()
    {
        if (cutsceneProgress > textBoxObjects.GetUpperBound(0))
        {
            GameManager.gameManager.ChangeScene(GameManager.GameScenes.InGame);
            return;
        }
        foreach (GameObject cutsceneObject in textBoxObjects)
        {
            cutsceneObject.SetActive(false);
        }
        if (cutsceneProgress <= textBoxObjects.GetUpperBound(0))
        {
            textBoxObjects[cutsceneProgress].SetActive(true);
        } else
        {
            GameManager.gameManager.ChangeScene(GameManager.GameScenes.InGame);
        }
    }
}
