using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
        // Registrierung beim SceneLoaded-Event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        Debug.Log("Szene geladen: " + scene.name);
        // Hier kannst du deinen Code ausführen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
