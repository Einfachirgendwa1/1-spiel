using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sounds {
    public class GunSound {
        private const int SpeedMetersPerSecond = 350;
        private const int ObstructionMask = 1 << 9 | 1 << 10; // whatIsWall + whatIsGround

        private static readonly List<GunSound> GunSounds = new();
        private float expansion;

        internal Vector3 Origin;

        internal int Loudness { get; private set; }

        internal static void Create(Vector3 origin, int initialLoudness) {
            GunSound sound = new() {
                Origin = origin, Loudness = initialLoudness
            };

            GunSounds.Add(sound);
        }

        internal static IEnumerable<GunSound> Hearable(Vector3 position) {
            return GunSounds.Where(sound => sound.CanBeHeard(position));
        }

        private bool CanBeHeard(Vector3 position) {
            Vector3 direction = position - Origin;
            bool obstructed = Physics.Raycast(Origin, direction.normalized, direction.magnitude, ObstructionMask);

            return direction.magnitude <= expansion && !obstructed;
        }

        internal void Update() {
            expansion += SpeedMetersPerSecond * Time.deltaTime;
        }
    }
}