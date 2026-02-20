using System;
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
    }
}