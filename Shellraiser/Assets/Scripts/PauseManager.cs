using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
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
