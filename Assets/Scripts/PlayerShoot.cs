using System;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    public static Action shootInput;

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
    }
}
