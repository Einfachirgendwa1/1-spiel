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


    Rigidbody playerRb;
    bool grounded;

    private void Start() {
        playerRb = GetComponent<Rigidbody>();
    }

    private void Update() {
        // handle drag
        if (grounded) {
            playerRb.linearDamping = groundDrag;
        } else {
            playerRb.linearDamping = 0;
        }
    }

    private void FixedUpdate() {
        Vector3 forward = transform.rotation * Vector3.forward;
        Vector3 right = transform.rotation * Vector3.right;

        // calculate movement direction
        Vector3 moveDirection = forward * Input.GetAxisRaw("Vertical") + right * Input.GetAxisRaw("Horizontal");

        // calculate force
        Vector3 force = moveSpeed * 500f * moveDirection.normalized;
        // in air, apply air multiplier
        if (!grounded) {
            force *= airMultiplier;
        }

        playerRb.AddForce(force, ForceMode.Force);

        if (Input.GetKey(jumpKey) && grounded) {
            playerRb.AddForce(transform.up * jumpForce * 10, ForceMode.Impulse);
            Debug.Log("jump");
        }
    }

    bool CollisionWithGround(Collision collision) => (whatIsGround & (1 << collision.gameObject.layer)) != 0;

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            grounded = true;
        }
    }

    void OnCollisionExit(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            grounded = false;
        }
    }
}