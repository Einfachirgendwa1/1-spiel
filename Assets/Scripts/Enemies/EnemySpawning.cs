using NUnit.Framework;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public GameObject enemy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(Transform Child in transform)
        {
            Instantiate(enemy, Child.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
