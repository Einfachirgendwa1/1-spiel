using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float MovementSpeed = .07F;

    public Vector2 MouseSensitivity = Vector2.one;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update() {
        static int taste(KeyCode key) => Input.GetKey(key) ? 1 : 0;

        Vector3 vorne = new(Vector3.forward.x, 0, Vector3.forward.z);
        Vector3 links = new(Vector3.left.x, 0, Vector3.left.z);

        transform.Translate(transform.rotation * vorne * (taste(KeyCode.W) - taste(KeyCode.S)) * MovementSpeed);
        transform.Translate(transform.rotation * links * (taste(KeyCode.A) - taste(KeyCode.D)) * MovementSpeed);

        float yaw = Input.GetAxis("Mouse X") * MouseSensitivity.x;
        float pitch = Input.GetAxis("Mouse Y") * MouseSensitivity.y;
        Vector3 rotation = new(yaw, pitch);

        transform.Rotate(rotation, Space.World);
    }
}
