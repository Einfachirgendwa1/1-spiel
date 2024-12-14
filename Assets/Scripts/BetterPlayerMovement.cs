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


        if (Input.GetKey(jumpKey) && grounded) {
            playerRb.AddForce(transform.up * jumpForce * 10, ForceMode.Impulse);
            Debug.Log("jump");
        }
    }
}