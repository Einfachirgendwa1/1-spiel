using System.Collections;
using UnityEngine;

namespace Gun {
    public class Finished {
        public bool IsFinished = true;
    }

    public abstract class GunCallbacks : MonoBehaviour {
        public readonly Finished Finished = new();

        public virtual IEnumerator StartShoot() {
            Finished.IsFinished = true;
            yield return null;
        }

        public virtual IEnumerator StartReload() {
            Finished.IsFinished = true;
            yield return null;
        }

        public virtual IEnumerator StartEquip() {
            Finished.IsFinished = true;
            yield return null;
        }

        public virtual IEnumerator StartUnequip() {
            Finished.IsFinished = true;
            yield return null;
        }
    }
}