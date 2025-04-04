using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [Header("Movement")]
    public float movementSpeed = 20;
    public float acceleration = 0.1F;

    public float jumpForce = 4;

    [Header("Collision")]
    public LayerMask whatIsGround;


    Rigidbody playerRb;
    bool grounded;

    public void Start() {
        playerRb = GetComponent<Rigidbody>();
    }

    public void Update() {
        if (Input.GetKey(KeyCode.Space) && grounded) {
            playerRb.AddForce(new(0, jumpForce, 0), ForceMode.Impulse);
            grounded = false;
        }
    }

    RaycastHit RecheckGrounded() {
        grounded = Physics.Raycast(transform.position, Vector3.down, out RaycastHit raycasthit, 1.0F, whatIsGround);
        return raycasthit;
    }

    Vector3 MovementDirection(Vector3 plane) {
        Vector3 moveDirection = Vector3.forward * Input.GetAxisRaw("Vertical") + Vector3.right * Input.GetAxisRaw("Horizontal");
        Vector3 direction = (transform.rotation * moveDirection).normalized;
        return Vector3.ProjectOnPlane(direction, plane) * movementSpeed;
    }
    Vector3 Movement(Vector3 src, Vector3 dst) => Vector3.Lerp(src, dst, acceleration);

    public void FixedUpdate() {
        Vector3 plane = RecheckGrounded().normal;
        Vector3 src = playerRb.linearVelocity;
        Vector3 dst = MovementDirection(plane);

        src.y = 0;
        dst.y = 0;
        Vector3 movement = Movement(src, dst);

        movement.y = playerRb.linearVelocity.y;
        playerRb.linearVelocity = movement;
    }
}