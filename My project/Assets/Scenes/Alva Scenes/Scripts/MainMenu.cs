using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuButtons;

    public void NewGame()
    { 
        SceneManager.LoadScene("MainScene"); // din spelscen
    }
    public void LoadGame()
    {
        SaveManager.Instance.LoadGame(); // ladda sparfil
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
