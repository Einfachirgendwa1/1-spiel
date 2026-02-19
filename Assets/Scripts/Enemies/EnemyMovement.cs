using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;
using Validation;

namespace Enemies {
    public class EnemyMovement : MonoBehaviour {

        private static readonly int Speed = Animator.StringToHash("Speed");

        [NonNull] public Animator enemyAnim;
        [NonNull] public NavMeshAgent agent;
        public float maxSpeed = 5;
        internal float currentSpeed;

        internal Vector3 Destination;

        public void Start()
        {
            enemyAnim = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
        }
        private void Update() {
            agent.destination = Destination;
            agent.speed = currentSpeed;
           

            if (agent.velocity.magnitude > 0.1f) {
                enemyAnim.SetFloat(Speed, agent.velocity.magnitude/maxSpeed);
            }
        }
    }
}