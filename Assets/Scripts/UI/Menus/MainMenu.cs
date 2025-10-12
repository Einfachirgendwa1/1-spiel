using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Menus {
    public class MainMenu : MonoBehaviour {
        public void StartGame() {
            SceneManager.LoadScene("Main");
            Menu.Cursor(false);
        }

        public void QuitGame() {
            Application.Quit();
        }
    }
}