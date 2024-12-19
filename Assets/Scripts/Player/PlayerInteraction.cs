using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Camera playerCamera;

    [SerializeField] float distance = 1f;

    [SerializeField] Vector3 offset = new Vector3(0, 0, 0.6f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("PlayerInteraction is running");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
 
            RaycastHit interacion;
            if (Physics.Raycast(playerCamera.transform.position + offset, playerCamera.transform.forward, out interacion , distance))
            {
                Debug.Log("player interact " + interacion.transform.name);
                Interact target = interacion.transform.GetComponent<Interact>();
                if (target != null)
                {
                    
                    target.Interaction();
                }
            }
        }
    }
}
