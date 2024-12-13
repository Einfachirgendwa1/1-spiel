using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float MovementSpeed = .07F;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update() {
        // Movement
        static int taste(KeyCode key) => Input.GetKey(key) ? 1 : 0;

        Vector3 vorne = new(Vector3.forward.x, 0, Vector3.forward.z);
        Vector3 links = new(Vector3.left.x, 0, Vector3.left.z);

        transform.Translate(transform.rotation * vorne * (taste(KeyCode.W) - taste(KeyCode.S)) * MovementSpeed);
        transform.Translate(transform.rotation * links * (taste(KeyCode.A) - taste(KeyCode.D)) * MovementSpeed);
    }
}
