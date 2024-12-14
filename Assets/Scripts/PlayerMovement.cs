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

    private void FixedUpdate() {

        grounded = Physics.Raycast(transform.position, Vector3.down, GetComponent<CapsuleCollider>().height / 2 + 0.1f, whatIsGround);

        // handle drag
        if (grounded) {
            playerRb.linearDamping = groundDrag;
        } else {
            playerRb.linearDamping = 0;
        }

        Vector3 moveDirection = Vector3.forward * Input.GetAxisRaw("Vertical") + Vector3.right * Input.GetAxisRaw("Horizontal");

        float currentMoveSpeed = moveSpeed * 500f;
        if (!grounded) {
            currentMoveSpeed *= airMultiplier;
        }

        playerRb.AddRelativeForce(moveDirection.normalized * currentMoveSpeed, ForceMode.Force);

        // Bewegungsgeschwindigkeit auf der XZ-Achse limitieren
        Vector2 planeMovement = new(playerRb.linearVelocity.x, playerRb.linearVelocity.z);
        if (planeMovement.magnitude > moveSpeed) {
            // Wir sind zu schnell, eigentliche Geschwindigkeit bilden und anwenden
            Vector2 limited = planeMovement.normalized * moveSpeed;
            playerRb.linearVelocity = new Vector3(limited.x, playerRb.linearVelocity.y, limited.y);
        }

        if (Input.GetKey(jumpKey) && grounded) {
            playerRb.AddRelativeForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}