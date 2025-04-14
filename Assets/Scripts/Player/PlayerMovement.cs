using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [Header("Movement")] public float movementSpeed = 20;

    public float jumpForce = 4;

    [Header("Collision")] public LayerMask whatIsGround;

    public float movementAcceleration = 20;
    public bool grounded = true;
    public new Rigidbody rigidbody;

    public void Start() {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Update() {
        if (Input.GetKey(KeyCode.Space) && grounded) {
            rigidbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            grounded = false;
        }
    }

    public void FixedUpdate() {
        Vector3 plane = RecheckGrounded().normal;
        Vector3 src = rigidbody.linearVelocity;
        Vector3 dst = MovementDirection(plane);

        src.y = 0;
        dst.y = 0;
        Vector3 movement = MovementVector(src, dst);

        movement.y = rigidbody.linearVelocity.y;
        rigidbody.linearVelocity = movement;
    }

    private Vector3 MovementVector(Vector3 src, Vector3 dst) {
        return Vector3.Lerp(src, dst, movementAcceleration);
    }


    private RaycastHit RecheckGrounded() {
        grounded = Physics.Raycast(transform.position, Vector3.down, out RaycastHit raycasthit, 1.0F, whatIsGround);
        return raycasthit;
    }

    private Vector3 MovementDirection(Vector3 plane) {
        Vector3 moveDirection = Vector3.forward * Input.GetAxisRaw("Vertical") +
                                Vector3.right * Input.GetAxisRaw("Horizontal");
        Vector3 direction = (transform.rotation * moveDirection).normalized;
        return Vector3.ProjectOnPlane(direction, plane) * movementSpeed;
    }
}