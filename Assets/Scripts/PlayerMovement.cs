//using UnityEngine;

//public class PlayerMovement : MonoBehaviour {
//    public float MovementSpeed = .07F;

//    public Vector2 MouseSensitivity = Vector2.one;

//    float UnterUns;

//    bool AufBoden() => Physics.Raycast(transform.position, Vector3.down, UnterUns);

//    void Start() {
//        Cursor.lockState = CursorLockMode.Locked;
//        Cursor.visible = false;

//        UnterUns = GetComponent<Collider>().bounds.extents.y + 0.1F;
//    }

//    void Update() {
//        static int taste(KeyCode key) => Input.GetKey(key) ? 1 : 0;

//        Vector3 vorne = new(Vector3.forward.x, 0, Vector3.forward.z);
//        Vector3 links = new(Vector3.left.x, 0, Vector3.left.z);

//        transform.Translate(transform.rotation * vorne * (taste(KeyCode.W) - taste(KeyCode.S)) * MovementSpeed);
//        transform.Translate(transform.rotation * links * (taste(KeyCode.A) - taste(KeyCode.D)) * MovementSpeed);

//        float yaw = Input.GetAxis("Mouse X") * MouseSensitivity.x;
//        float pitch = Input.GetAxis("Mouse Y") * MouseSensitivity.y;

//        transform.Rotate(yaw * Vector3.up, Space.World);
//        transform.Rotate(pitch * Vector3.left, Space.Self);

//        if (Input.GetKey(KeyCode.Space) && AufBoden()) {
//            //todo
//        }
//    }
//}
