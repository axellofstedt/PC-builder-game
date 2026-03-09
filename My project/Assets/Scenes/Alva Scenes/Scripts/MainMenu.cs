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
        SaveManager.Instance.loadBool = true;
        SceneManager.LoadScene("MainScene"); // din spelscen
        // SaveManager.Instance.LoadGame() ska kallas på i ett empty gameobject i MainScene, så att den laddar spelet när scenen startar
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
