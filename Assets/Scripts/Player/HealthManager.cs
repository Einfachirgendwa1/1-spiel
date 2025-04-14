using UnityEngine;

public class HealthManager : MonoBehaviour, IDamageablePlayer {
    public float healthPoints;

    public void GetHurt(float damage) {
        healthPoints -= damage;
        if (healthPoints <= 0) {
            Debug.Log("Player died");
        }
    }
}