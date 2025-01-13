using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [Header("Movement")]
    public float movementSpeed = 10;
    public float movementSpeedMultiplier = 500;
    public float jumpForce = 400;
    public float movePercentageGround = 100;
    public float movePercentageAir = 30;

    [Header("Collision")]
    public LayerMask whatIsGround;


    Rigidbody playerRb;

    private void Start() {
        playerRb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        bool grounded = Physics.Raycast(transform.position, Vector3.down, out RaycastHit raycasthit, 1.5F, whatIsGround);
        Vector3 plane = raycasthit.normal;

        Vector3 moveDirection = Vector3.forward * Input.GetAxisRaw("Vertical") + Vector3.right * Input.GetAxisRaw("Horizontal");
        Vector3 direction = (transform.rotation * moveDirection).normalized;

        if (grounded) {
            movementSpeedMultiplier = 1000;
        } else {
            movementSpeedMultiplier = 1000 / 3;
        }

        playerRb.AddForce(movementSpeed * movementSpeedMultiplier * Vector3.ProjectOnPlane(direction, plane), ForceMode.Force);
    }
}