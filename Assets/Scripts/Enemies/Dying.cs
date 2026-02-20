using Targeting;
using UnityEngine;
using Validation;

namespace Enemies {
    public class Dying : MonoBehaviour {
        private static readonly int IsDead = Animator.StringToHash("isDead");

        [NonNull] public Animator animator;
        [NonNull] public Health health;

        private void Update() {
            bool isDead = health.health <= 0;
            animator.SetBool(IsDead, isDead);

            if (isDead) {
                foreach (MonoBehaviour component in gameObject.GetComponentsInChildren<MonoBehaviour>()) {
                    component.enabled = false;
                }
            }
        }
    }
}