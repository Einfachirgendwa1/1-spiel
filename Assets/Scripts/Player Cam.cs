using UnityEngine;

public class PlayerCam : MonoBehaviour {
    public float sensX;
    public float sensY;

    float xRotation;
    float yRotation;

    Transform player;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        player = transform.parent.parent;
    }

    void Update() {
        // get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        // ist yRotation + mouseX richtig oder is da was vertauscht bzw. falsch benannt?
        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // rotate cam and orientation
        player.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
}
