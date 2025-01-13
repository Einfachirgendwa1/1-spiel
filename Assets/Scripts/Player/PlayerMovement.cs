using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [Header("Movement")]
    public float movementSpeed = 10;
    public float jumpForce = 400;
    public float movePercentageGround = 100;
    public float movePercentageAir = 30;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode crouchKey = KeyCode.C;

    [Header("Collision")]
    public LayerMask whatIsGround;


    Rigidbody playerRb;
    CapsuleCollider capsuleCollider;

    private void Start() {
        playerRb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        playerRb.linearDamping = 6f;
    }

    private void FixedUpdate() {
        Physics.Raycast(transform.position, Vector3.down, out RaycastHit raycasthit, 1.5F, whatIsGround);
        Vector3 plane = raycasthit.normal;

        Vector3 moveDirection = Vector3.forward * Input.GetAxisRaw("Vertical") + Vector3.right * Input.GetAxisRaw("Horizontal");
        Vector3 direction = (transform.rotation * moveDirection).normalized;

        playerRb.AddForce(Vector3.ProjectOnPlane(direction, plane) * movementSpeed * 500, ForceMode.Force);
    }
}