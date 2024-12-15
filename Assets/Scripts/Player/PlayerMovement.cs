using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [Header("Movement")]
    public float moveSpeed;

    public float jumpForce;
    public float movePercentageGround;
    public float movePercentageAir;

    [Header("Crouching")]
    public Vector3 crouchedScale;
    public float crouchedMoveSpeed;

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
    }

    float RaycastLength() => capsuleCollider.height / 2 + 0.1F;

    private void FixedUpdate() {
        // Raycast nach unten um zu testen ob wir auf etwas stehen.
        // Zum Beispiel auf Männer.
        bool grounded = Physics.Raycast(transform.position, Vector3.down, RaycastLength(), whatIsGround);
        bool crouching = Input.GetKey(crouchKey);

        if (crouching) {
            transform.localScale = crouchedScale;
        }

        transform.localScale = crouching ? crouchedScale : new(1, 1, 1);
        float current_speed = crouching ? crouchedMoveSpeed : moveSpeed;

        // Richtung in die wir uns bewegen wollen.
        Vector3 moveDirection = Vector3.forward * Input.GetAxisRaw("Vertical") + Vector3.right * Input.GetAxisRaw("Horizontal");
        // Richtung in die wir uns *wirklich* bewegen wollen.
        moveDirection = (transform.rotation * moveDirection).normalized;

        // Wir versuchen nun also unsere aktuelle velocity an die moveDirection anzunähern.
        // Jedoch hängt die Trägheit des Spielers davon ab, ob wir in der Luft oder auf dem Boden sind.
        // Je weniger, desto träger.
        float changeFactor = (grounded ? movePercentageGround : movePercentageAir) / 100;

        // Jetzt berechnen wir die tatsächliche velocity
        Vector3 newVelocity = Vector3.Lerp(playerRb.linearVelocity.normalized, moveDirection, changeFactor) * current_speed;
        newVelocity.y = playerRb.linearVelocity.y;

        playerRb.linearVelocity = newVelocity;

        // Springen wenn wir springen wollen
        if (Input.GetKey(jumpKey) && grounded) {
            playerRb.AddRelativeForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }


    }
}