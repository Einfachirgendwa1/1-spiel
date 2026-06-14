using Guns;
using Targeting;
using UnityEngine;
using UnityEngine.AI;
using Validation;

namespace Enemies {
    public class Dying : MonoBehaviour {
        private static readonly int IsDead = Animator.StringToHash("isDead");

        [NonNull] public Animator animator;
        [NonNull] public Health health;

        private void Update() {
            bool isDead = health.health <= 0;
            animator.SetBool(IsDead, isDead);

            if (!isDead) return;
            animator.SetLayerWeight(1, 0);

            GetComponent<NavMeshAgent>().isStopped = true;
            GetComponent<GunController>().enabled = false;
            GetComponentsInChildren<Collider>().ForEach(coll => coll.enabled = false);
        }
    }
}