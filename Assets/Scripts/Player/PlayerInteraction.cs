using UnityEngine;

interface IInteractable
{
    public void Interact();
}
public class PlayerInteraction : MonoBehaviour
{
    public Camera playerCamera;

    [SerializeField] float distance = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
 
            RaycastHit interacion;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out interacion , distance, ~(1<<6)))
            {
                if (interacion.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact();
                }
            }
        }
    }
}
