using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuButtons;

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene"); // din spelscen
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
    public GameObject settingsPanel;

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        mainMenuButtons.SetActive(false);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        mainMenuButtons.SetActive(true);
    }
}
