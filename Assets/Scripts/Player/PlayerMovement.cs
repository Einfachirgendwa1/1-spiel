using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [Header("Movement")]
    public float uncrouchedMoveSpeed = 10;
    public float jumpForce = 400;
    public float movePercentageGround = 100;
    public float movePercentageAir = 30;

    [Header("Misc")]
    public Vector3 uncrouchedScale = new(1, 1, 1);

    [Header("Crouching")]
    public Vector3 crouchedScale = new(1, 0.5F, 1);
    public float crouchedMoveSpeed = 30;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode crouchKey = KeyCode.C;

    [Header("Collision")]
    public LayerMask whatIsGround;


    Rigidbody playerRb;
    CapsuleCollider capsuleCollider;
    float currentSpeed;

    [SerializeField] bool grounded;

    private void Start() {
        playerRb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        playerRb.linearDamping = 6f;

        currentSpeed = uncrouchedMoveSpeed;
    }

    private void Update() {
        if (Input.GetKeyDown(crouchKey)) {
            // Wir beginnen zu crouchen
            transform.localScale = crouchedScale;
            currentSpeed = crouchedMoveSpeed;

            // Wir werden kleiner, und damit wir nach dem kleiner werden nicht nach unten fallen,
            // teloportieren wir uns einfach nach unten.
            Vector3 position = transform.position;
            position.y -= capsuleCollider.height / 4;
            transform.position = position;
        } else if (Input.GetKeyUp(crouchKey)) {
            // Wir hören auf zu crouchen
            transform.localScale = uncrouchedScale;
            currentSpeed = uncrouchedMoveSpeed;

            // Damit wir nicht im Boden stecken bleiben nach dem größer werden, nach oben teleportieren.
            Vector3 position = transform.position;
            position.y += capsuleCollider.height / 4;
            transform.position = position;
        }

        // Springen wenn wir springen wollen
        if (Input.GetKeyDown(jumpKey) && grounded) {
            Jump();
        }
    }

    private void FixedUpdate() {
        grounded = Physics.Raycast(transform.position, Vector3.down, out RaycastHit raycasthit, 1.5F, whatIsGround);

        Vector3 moveDirection = transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal");
        moveDirection = Vector3.ProjectOnPlane((transform.rotation * moveDirection).normalized, raycasthit.normal);

        playerRb.AddForce(moveDirection.normalized * currentSpeed, ForceMode.Acceleration);
    }

    void Jump()
    {
        playerRb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
}