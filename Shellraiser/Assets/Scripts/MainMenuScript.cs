using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void OnPlayGame()
    {
        SceneManager.LoadScene("Gameplay"); 
    }

    public void OnCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void OnBackButton()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void OnQuitGame()
    {
        Application.Quit();
        Debug.Log("You quit game!");
    }
}
