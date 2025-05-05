using System.Collections;
using UnityEngine;

namespace GunCallbacks {
    public class Pistol : Gun.GunCallbacks {
        private Animator animator;

        public void Start() {
            animator = GetComponent<Animator>();
        }


        public void AnimationFinished() {
            Finished.IsFinished = true;
            Debug.Log("Animation Finished");
            animator.Play("Idle");
        }

        public override IEnumerator StartShoot() {
            animator.Play("Shoot");
            yield return null;
        }

        public override IEnumerator StartReload() {
            animator.Play("Reload");
            yield return null;
        }
    }
}