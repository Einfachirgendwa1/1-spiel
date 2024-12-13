using UnityEngine;

public class PlayerCollisionTracker : MonoBehaviour {
    public bool groundCollisionLastFrame = false;

    void OnCollisionEnter(Collision collision) {
        groundCollisionLastFrame = true;
    }

    void OnCollisionExit(Collision collision) {
        if (collision.collider.name == "Ground") {
            groundCollisionLastFrame = false;
        }
    }
}
