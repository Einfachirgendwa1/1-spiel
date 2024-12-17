using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour {
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake() {
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void SearchWalkPoint() {
        //Calculate random point in range
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomY = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y + randomY);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) {
            walkPointSet = true;
        }
    }

    private void Patroling() {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet) {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f) {
            walkPointSet = false;
        }
    }

    private void ChasePlayer() {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer() {
        // enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked) {
            //Attack Code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack() {
        alreadyAttacked = false;
    }

    void Start() {

    }

    // Update is called once per frame
    void Update() {
        //check for attack and sight range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) {
            Patroling();
        } else if (playerInSightRange && !playerInAttackRange) {
            ChasePlayer();
        } else {
            AttackPlayer();
        }
    }
}
