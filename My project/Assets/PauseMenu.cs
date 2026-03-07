using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public FirstPersonController firstPersonController;
    public GameObject settingsPanel;
    public GameObject reticle;
    public GameObject menuButtons;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && ModeManager.Instance.currentMode == GameMode.Player)
        {
            if (Time.timeScale == 1) Pause();
            else if (!settingsPanel.activeSelf) Resume();
        }
    }

    private void Pause()
    {
        Time.timeScale = 0;
        pauseMenuPanel.SetActive(true);
        firstPersonController.enabled = false;

        // Unlock the cursor and make it visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        reticle.SetActive(false);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenuPanel.SetActive(false);
        firstPersonController.enabled = true;

        // Lock the cursor and make it invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        reticle.SetActive(true);
    }

    public void QuitToMainMenu()
    {
        Resume(); // Ensure the game is resumed before loading the main menu
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenuScene");
    }
    

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        menuButtons.SetActive(false);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        menuButtons.SetActive(true);
    }
}
