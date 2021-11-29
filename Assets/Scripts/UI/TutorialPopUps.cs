using UnityEngine;

public class TutorialPopUps : MonoBehaviour
{
    public GameObject tutorialPopUp;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == GameManager.gameManager.player)
        {
            tutorialPopUp.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == GameManager.gameManager.player)
        {
            tutorialPopUp.SetActive(false);
        }
    }
}
