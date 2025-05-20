using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void StartGame() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene("Main");
    }

    public void QuitGame() {
        Debug.Log("Quit!");
        Application.Quit();
    }

}
