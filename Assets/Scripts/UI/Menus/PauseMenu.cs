using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Menus {
    public class PauseMenu : MonoBehaviour {
        public GameObject pauseMenuUI;
        private bool paused;

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                TogglePause();
            }
        }

        public void TogglePause() {
            Time.timeScale = paused ? 0f : 1f;
            paused = !paused;

            pauseMenuUI.SetActive(paused);
            Menu.Cursor(paused);
        }

        public void SettingsMenu() { }

        public void QuitToMenu() {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }
    }
}