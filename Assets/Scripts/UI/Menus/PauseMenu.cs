using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Menus {
    public class PauseMenu : MonoBehaviour {
        private readonly List<GameObject> children = new();
        private bool paused = true;

        private void Start() {
            foreach (Transform child in transform) {
                children.Add(child.gameObject);
            }

            TogglePause();
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                TogglePause();
            }
        }

        public void TogglePause() {
            paused = !paused;

            Time.timeScale = paused ? 0f : 1f;
            Menu.Cursor(paused);
            children.ForEach(go => go.SetActive(paused));
        }

        public void SettingsMenu() { }

        public void QuitToMenu() {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }
    }
}