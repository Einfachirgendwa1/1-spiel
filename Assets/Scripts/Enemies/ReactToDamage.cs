using Targeting;
using UnityEngine;
using Validation;

namespace Enemies {
    public class ReactToDamage : MonoBehaviour {
        [NonNull] public Health health;
        [NonNull] public EnemyPlayerDetection detection;
        [Positive] public int secondsInHighAlert;

        private void Start() {
            health.OnDamageTaken += _ => detection.ConsiderHighAlert(secondsInHighAlert);
        }
    }
}