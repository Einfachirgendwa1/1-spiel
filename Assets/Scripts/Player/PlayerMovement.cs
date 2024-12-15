using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [Header("Movement")]
    public float uncrouchedMoveSpeed = 10;
    public float jumpForce = 230;
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
    bool grounded = false;

    private void Start() {
        playerRb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

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
            playerRb.AddRelativeForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    float RaycastLength() => capsuleCollider.height * transform.localScale.y / 2 + 0.05F;

    private void FixedUpdate() {
        // Raycast nach unten um zu testen ob wir auf etwas stehen.
        // Zum Beispiel auf Männer.
        grounded = Physics.Raycast(transform.position, Vector3.down, RaycastLength(), whatIsGround);

        // Richtung in die wir uns bewegen wollen.
        Vector3 moveDirection = Vector3.forward * Input.GetAxisRaw("Vertical") + Vector3.right * Input.GetAxisRaw("Horizontal");

        // Richtung in die wir uns *wirklich* bewegen wollen.
        moveDirection = (transform.rotation * moveDirection).normalized;

        // Wir versuchen nun also unsere aktuelle velocity an die moveDirection anzunähern.
        // Jedoch hängt die Trägheit des Spielers davon ab, ob wir in der Luft oder auf dem Boden sind.
        // Je weniger, desto träger.
        float changeFactor = (grounded ? movePercentageGround : movePercentageAir) / 100;

        // Jetzt berechnen wir die tatsächliche velocity
        Vector3 newVelocity = Vector3.Lerp(playerRb.linearVelocity.normalized, moveDirection, changeFactor) * currentSpeed;
        newVelocity.y = playerRb.linearVelocity.y;


        playerRb.linearVelocity = newVelocity;
    }
}