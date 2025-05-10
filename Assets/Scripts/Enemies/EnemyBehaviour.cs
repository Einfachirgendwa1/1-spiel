using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour {
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    // Patrolling
    public Vector3 walkPoint;
    public float walkPointRange;

    // Attacking
    public float timeBetweenAttacks;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    private bool alreadyAttacked;
    private bool walkPointSet;

    private void Awake() {
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update() {
        //check for attack and sight range
        playerInSightRange = (transform.position - player.position).magnitude <= sightRange;
        playerInAttackRange = (transform.position - player.position).magnitude <= attackRange;

        if (!playerInSightRange && !playerInAttackRange) {
            Patrolling();
        } else if (playerInSightRange && !playerInAttackRange) {
            ChasePlayer();
        } else {
            AttackPlayer();
        }
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

    private void Patrolling() {
        if (!walkPointSet) {
            SearchWalkPoint();
        } else {
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
        // enemy stops
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (alreadyAttacked) {
            return;
        }
        //Attack Code

        alreadyAttacked = true;
        Invoke(nameof(ResetAttack), timeBetweenAttacks);
    }

    private void ResetAttack() {
        alreadyAttacked = false;
    }
}