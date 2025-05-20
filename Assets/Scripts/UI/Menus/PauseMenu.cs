using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;

    void Update() {
        if (gameIsPaused && pauseMenuUI != null) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } else {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (gameIsPaused == true) {
                Resume();
            } else {
                Pause();
            }

        }
    }
    void Resume() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void ResumeGame() {
        Resume();
    }

    public void SettingsMenu() {
        //nothing to see here
    }

    public void QuitToMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
