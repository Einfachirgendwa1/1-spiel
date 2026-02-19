using UnityEngine;
using UnityEngine.AI;
using Validation;

namespace Enemies {
    public class EnemyMovement : MonoBehaviour {
        private static readonly int Speed = Animator.StringToHash("Speed");

        [NonNull] public Animator enemyAnim;
        [NonNull] public NavMeshAgent agent;
        public float maxSpeed = 5;
        internal float CurrentSpeed;
        internal Vector3 Destination;

        private void Update() {
            agent.destination = Destination;
            agent.speed = CurrentSpeed;

            if (agent.velocity.magnitude > 0.1f) {
                enemyAnim.SetFloat(Speed, agent.velocity.magnitude / maxSpeed);
            }
        }
    }
} 