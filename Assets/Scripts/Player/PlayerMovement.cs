using UnityEngine;

public class PlayerMovement : Movement.Movement {
    [Header("Movement")]
    public float movementSpeed = 20;
    public float acceleration = 0.1F;

    public float jumpForce = 4;

    [Header("Collision")]
    public LayerMask whatIsGround;

    protected override LayerMask WhatIsGround => whatIsGround;


    public void Start() {
        rigidbody = GetComponent<Rigidbody>();
        movementAcceleration = acceleration;
    }

    public void Update() {
        if (Input.GetKey(KeyCode.Space) && grounded) {
            rigidbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            grounded = false;
        }
    }

    protected override Vector3 MovementDirection(Vector3 plane) {
        Vector3 moveDirection = Vector3.forward * Input.GetAxisRaw("Vertical") + Vector3.right * Input.GetAxisRaw("Horizontal");
        Vector3 direction = (transform.rotation * moveDirection).normalized;
        return Vector3.ProjectOnPlane(direction, plane) * movementSpeed;
    }
}