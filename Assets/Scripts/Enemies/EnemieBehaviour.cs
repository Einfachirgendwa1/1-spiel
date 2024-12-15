using UnityEngine;
using UnityEngine.AI;

public class EnemieBehaviour : MonoBehaviour {
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public NavMeshAgent agent;

    public Transform player;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;

    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
