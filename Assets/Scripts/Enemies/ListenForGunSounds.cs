using System.Linq;
using Sounds;
using UnityEngine;

namespace Enemies {
    public class ListenForGunSounds : MonoBehaviour {
        public int loudnessThreshold;

        internal Vector3? TargetPosition;

        private void Update() {
            GunSound loudest = GunSound.Hearable(transform.position).OrderBy(sound => sound.Loudness).FirstOrDefault();
            TargetPosition = loudest is not null && loudest.Loudness >= loudnessThreshold ? loudest.Origin : null;
        }
    }
}