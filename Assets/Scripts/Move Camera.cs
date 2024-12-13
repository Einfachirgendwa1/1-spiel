using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = cameraPosition.position;
    }
}
