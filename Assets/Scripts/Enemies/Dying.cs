using Targeting;
using UnityEngine;
using Validation;

namespace Enemies {
    public class Dying : MonoBehaviour {
        private static readonly int IsDead = Animator.StringToHash("isDead");

        [NonNull] public Animator animator;
        [NonNull] public Health health;

        private void Update() {
            animator.SetBool(IsDead, health.health <= 0);
        }
    }
}