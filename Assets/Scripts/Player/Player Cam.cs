using UnityEngine;

public class PlayerCam : MonoBehaviour {
    public float sensX;
    public float sensY;

    private Transform player;

    private float xRotation;
    private float yRotation;

    private void Start() {
        // Cursor verstecken und im Fenster locken
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Unseren Opa holen
        player = transform.parent.parent;
    }

    private void Update() {
        // Maus Input lesen und anpassen
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        // Das sieht so kaputt aus, ist aber richtig.
        // Euler Winkel sind dumm.
        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Den Spieler rotieren, aber NICHT AUF DER Y-ACHSE, NUR AUF XZ
        player.rotation = Quaternion.Euler(0, yRotation, 0);
        // Den Camera Holder und alle seine Kinder rotieren, und zwar auf allen Achsen.
        // Die Kamera darf zwar nach unten schauen, aber nicht der gesamte Spieler.
        transform.parent.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
}