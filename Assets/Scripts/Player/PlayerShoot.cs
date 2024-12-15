using System;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    public static Action shootInput;
    public static Action reloadInput;

    [SerializeField] private KeyCode reloadKey;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            shootInput?.Invoke();
        }

        if (Input.GetKeyDown(reloadKey))
        {
            reloadInput?.Invoke();
        }
    }
}
