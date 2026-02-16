using UnityEngine;
using UnityEngine.AI;
using Validation;

namespace Enemies {
    public class EnemyMovement : MonoBehaviour {
        private static readonly int Speed = Animator.StringToHash("Speed");

        [NonNull] public Animator animator;
        [NonNull] public NavMeshAgent agent;

        internal Vector3 Destination;

        private void Update() {
            agent.destination = Destination;

            if (agent.velocity.magnitude > 0.1f) {
                animator.SetFloat(Speed, agent.velocity.magnitude);
            }
        }
    }
}