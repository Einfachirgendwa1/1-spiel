using System;
using System.Collections;
using UnityEngine;
using Validation;

namespace Targeting {
    public class Health : MonoBehaviour {
        [PositiveNonZero] public float health;
        [NonNull] public AudioSource audioSource;

        internal event Action OnDamageTaken;

        internal void DamageTakenCallback() {
            OnDamageTaken?.Invoke();
        }

        internal IEnumerator Kill() {
            yield return new WaitForSeconds(30f);
            Destroy(gameObject);
        }
    }
}