using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyPlayerDetection : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject playerReference;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerReference = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    private void Update()
    {
        TurnToPlayer();
    }

    IEnumerator FOVRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                }
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }

    }

    void TurnToPlayer()
    {
        // warum funktioniert das nicht???
        Vector3 directionToPlayer = (playerReference.transform.position - transform.position).normalized;
        if (canSeePlayer)
        {
            transform.rotation = Quaternion.Euler(0f, directionToPlayer.y, 0f);  
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0, 0f);
        }
    }
}
