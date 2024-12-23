using System.Collections.Generic;
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
    int groundCollisions = 0;
    DebugPrinter printer;
    readonly List<int> groundIds = new();

    [SerializeField] bool isOnGround;

    private void Start() {
        playerRb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        currentSpeed = uncrouchedMoveSpeed;
        printer = Printer.NewPrinter();
    }

    bool Grounded() => groundCollisions != 0;

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
            // Wir h�ren auf zu crouchen
            transform.localScale = uncrouchedScale;
            currentSpeed = uncrouchedMoveSpeed;

            // Damit wir nicht im Boden stecken bleiben nach dem gr��er werden, nach oben teleportieren.
            Vector3 position = transform.position;
            position.y += capsuleCollider.height / 4;
            transform.position = position;
        }

        // Springen wenn wir springen wollen
        if (Input.GetKeyDown(jumpKey) && Grounded()) {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate() {
        isOnGround = Grounded();
        printer.Print($"Ground Collisions rn: {groundCollisions}");

        // Richtung in die wir uns bewegen wollen.
        Vector3 moveDirection = Vector3.forward * Input.GetAxisRaw("Vertical") + Vector3.right * Input.GetAxisRaw("Horizontal");

        // Richtung in die wir uns *wirklich* bewegen wollen.
        moveDirection = (transform.rotation * moveDirection).normalized;

        // Wir versuchen nun also unsere aktuelle velocity an die moveDirection anzun�hern.
        // Jedoch h�ngt die Tr�gheit des Spielers davon ab, ob wir in der Luft oder auf dem Boden sind.
        // Je weniger, desto tr�ger.
        float changeFactor = (Grounded() ? movePercentageGround : movePercentageAir) / 100;

        // Jetzt berechnen wir die tats�chliche velocity
        Vector3 newVelocity = Vector3.Lerp(playerRb.linearVelocity.normalized, moveDirection, changeFactor) * currentSpeed;
        //newVelocity.y = Grounded() ? 0 : playerRb.linearVelocity.y;


        playerRb.linearVelocity = newVelocity;
    }

    bool InLayerMask(int layer, LayerMask mask) => (mask.value & (1 << layer)) != 0;

    bool IsGround(Collision collision) => InLayerMask(collision.gameObject.layer, whatIsGround) && collision.GetContact(0).point.y < transform.position.y;

    private void OnCollisionEnter(Collision collision) {
        if (IsGround(collision)) {
            groundIds.Add(collision.gameObject.GetInstanceID());
            groundCollisions++;
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (InLayerMask(collision.gameObject.layer, whatIsGround) && groundIds.Remove(collision.gameObject.GetInstanceID())) {
            groundCollisions--;
        }
    }
}