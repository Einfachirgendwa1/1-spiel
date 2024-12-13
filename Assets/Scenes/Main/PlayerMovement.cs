using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float movement_speed = 25;
    public Vector2 mouse_sensitivity = new(1, 1);

    Vector2 RelativeMouse() => new(Screen.height / 2 - Input.mousePosition.x, Screen.width / 2 - Input.mousePosition.y);

    void Bewegen(Vector3 wo) {
        transform.position += Time.deltaTime * movement_speed * wo;
    }

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update() {
        // Movement
        Vector3 nach(Vector3 wo) => transform.rotation * wo;
        int taste(KeyCode key) => Input.GetKey(key) ? 1 : 0;

        Bewegen(nach(Vector3.forward) * (taste(KeyCode.W) - taste(KeyCode.S)));
        Bewegen(nach(Vector3.left) * (taste(KeyCode.A) - taste(KeyCode.D)));

        // Rotation
        transform.rotation = Quaternion.AngleAxis(RelativeMouse().y * mouse_sensitivity.y, new(1, 0, 0));
    }
}
