using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [Header("Movement")]
    public float moveSpeed;

    public float jumpForce;
    public float movePercentageGround;
    public float movePercentageAir;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Collision")]
    public LayerMask whatIsGround;


    Rigidbody playerRb;
    float raycastLength;

    private void Start() {
        playerRb = GetComponent<Rigidbody>();
        raycastLength = GetComponent<CapsuleCollider>().height / 2 + 0.1f;
    }

    private void FixedUpdate() {
        // Raycast nach unten um zu testen ob wir auf etwas stehen.
        // Zum Beispiel auf M�nner.
        bool grounded = Physics.Raycast(transform.position, Vector3.down, raycastLength, whatIsGround);

        // Richtung in die wir uns bewegen wollen.
        Vector3 moveDirection = Vector3.forward * Input.GetAxisRaw("Vertical") + Vector3.right * Input.GetAxisRaw("Horizontal");
        // Richtung in die wir uns *wirklich* bewegen wollen.
        moveDirection = (transform.rotation * moveDirection).normalized;

        // Wir versuchen nun also unsere aktuelle velocity an die moveDirection anzun�hern.
        // Jedoch h�ngt die Tr�gheit des Spielers davon ab, ob wir in der Luft oder auf dem Boden sind.
        // Je weniger, desto tr�ger.
        float changeFactor = (grounded ? movePercentageGround : movePercentageAir) / 100;

        // Jetzt berechnen wir die tats�chliche velocity
        Vector3 newVelocity = Vector3.Lerp(playerRb.linearVelocity.normalized, moveDirection, changeFactor) * moveSpeed;
        newVelocity.y = playerRb.linearVelocity.y;

        playerRb.linearVelocity = newVelocity;

        // Springen wenn wir springen wollen
        if (Input.GetKey(jumpKey) && grounded) {
            playerRb.AddRelativeForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}