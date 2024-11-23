using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject storageButtons;
    private bool isPaused = false;

    void Update()
    {
        // Sprawdzamy, czy wciœniêto Escape
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!isPaused)
            {
                // Jeœli jest pauza, wracamy do gry
                pauseMenu.SetActive(true);
                storageButtons.SetActive(false);
                Time.timeScale = 0; // Wznawiamy czas
            }
            else
            {
                // Jeœli nie ma pauzy, wstrzymujemy grê
                pauseMenu.SetActive(false);
                storageButtons.SetActive(true);
                Time.timeScale = 1; // Zatrzymujemy czas
            }

            // Prze³¹czamy stan pauzy
            isPaused = !isPaused;
        }
    }

    public void OnResumeButton()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnMenuButton()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }

    public void OnQuitButton()
    {
        Application.Quit();
        Debug.Log("You quit game!");
    }
}
