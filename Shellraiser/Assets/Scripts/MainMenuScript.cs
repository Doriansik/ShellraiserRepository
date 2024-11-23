using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject volumeSlider;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject mainMenuButtonStorage;
    [SerializeField] private GameObject backButton;

    public void OnSettingsMenu()
    {
        volumeSlider.SetActive(true);
        backButton.SetActive(true);
        settingsMenu.SetActive(false);
        mainMenuButtonStorage.SetActive(false);
    }

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

    public void OnBackSettingsButton()
    {
        settingsMenu.SetActive(true);
        mainMenuButtonStorage.SetActive(true);
        volumeSlider.SetActive(false);
        backButton.SetActive(false);
    }

    public void OnQuitGame()
    {
        Application.Quit();
        Debug.Log("You quit game!");
    }
}
