using UnityEngine;

public class HealthManager : MonoBehaviour, IDamageablePlayer
{
    public float healthPoints;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetHurt(float damage)
    {
        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            Debug.Log("Player died");
        }
    }

}
