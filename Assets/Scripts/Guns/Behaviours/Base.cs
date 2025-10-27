using UnityEngine;

namespace Guns.Behaviours {
    public abstract class Base : StateMachineBehaviour {
        private float actualNormalizedTime;
        internal Gun gun;
        private float lastNormalizedTime;
        internal int repetitions;
        internal abstract State state { get; }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            repetitions = -1;
            gun = animator.gameObject.GetComponent<Gun>();

            gun.controller.state = state;

            if (!stateInfo.loop) {
                Action();
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (!animator.IsInTransition(layerIndex) && stateInfo.loop) {
                actualNormalizedTime += stateInfo.normalizedTime - lastNormalizedTime;

                int repsThisFrame = Mathf.FloorToInt(actualNormalizedTime);
                if (repsThisFrame > repetitions) {
                    repetitions = repsThisFrame;
                    Action();
                }
            }

            lastNormalizedTime = stateInfo.normalizedTime;
        }

        protected virtual void Action() {
            gun.controller.inputBuffer[state] = 0f;
        }
    }
}