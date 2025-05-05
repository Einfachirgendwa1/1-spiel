using System.Collections;
using UnityEngine;

namespace Gun {
    public class Finished {
        public bool IsFinished;
    }

    public abstract class GunCallbacks : MonoBehaviour {
        public virtual IEnumerator StartShoot(Finished finished) {
            finished.IsFinished = true;
            yield return null;
        }

        public virtual IEnumerator StartReload(Finished finished) {
            finished.IsFinished = true;
            yield return null;
        }

        public virtual IEnumerator StartEquip(Finished finished) {
            finished.IsFinished = true;
            yield return null;
        }

        public virtual IEnumerator StartUnequip(Finished finished) {
            finished.IsFinished = true;
            yield return null;
        }
    }
}