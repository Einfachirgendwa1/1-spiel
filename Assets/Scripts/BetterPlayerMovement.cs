using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Collision")]
    public LayerMask whatIsGround;


    Rigidbody rb;
    bool grounded;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        // handle drag
        if (grounded) {
            rb.linearDamping = groundDrag;
        } else {
            rb.linearDamping = 0;
        }
    }

    private void FixedUpdate() {
        Vector3 forward = transform.rotation * Vector3.forward;
        Vector3 right = transform.rotation * Vector3.right;

        // calculate movement direction
        Vector3 moveDirection = forward * Input.GetAxisRaw("Vertical") + right * Input.GetAxisRaw("Horizontal");

        // calculate force
        Vector3 force = moveSpeed * 10f * moveDirection.normalized;
        // in air, apply air multiplier
        if (!grounded) {
            force *= airMultiplier;
        }

        rb.AddForce(force, ForceMode.Force);

        if (Input.GetKey(jumpKey) && grounded) {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    bool CollisionWithGround(Collision collision) => (whatIsGround & (1 << collision.gameObject.layer)) != 0;

    void OnCollisionEnter(Collision collision) {
        if (CollisionWithGround(collision)) {
            grounded = true;
        }
    }

    void OnCollisionExit(Collision collision) {
        if (CollisionWithGround(collision)) {
            grounded = false;
        }
    }
}