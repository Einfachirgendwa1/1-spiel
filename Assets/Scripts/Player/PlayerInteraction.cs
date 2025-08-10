using UnityEngine;

internal interface IInteractable {
    public void Interact();
}

public class PlayerInteraction : MonoBehaviour {
    public Camera playerCamera;

    [SerializeField] private float distance = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start() { }

    // Update is called once per frame
    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            RaycastHit interacion;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out interacion,
                    distance, ~(1 << 6)))
                Debug.Log(interacion.transform.name);
                    {
                if (interacion.collider.gameObject.TryGetComponent(out IInteractable interactObj)) {
                    interactObj.Interact();
                }
            }
        }
    }
}