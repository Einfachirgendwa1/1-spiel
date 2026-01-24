using UnityEngine;

namespace Guns.Behaviours {
    public abstract class Base : StateMachineBehaviour {
        private float actualNormalizedTime;
        internal Gun Gun;
        private float lastNormalizedTime;
        internal int Repetitions;
        internal abstract State State { get; }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            Repetitions = -1;
            Gun = animator.gameObject.GetComponent<Gun>();

            Gun.Controller.State = State;

            if (!stateInfo.loop) {
                Action();
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (!animator.IsInTransition(layerIndex) && stateInfo.loop) {
                actualNormalizedTime += stateInfo.normalizedTime - lastNormalizedTime;

                int repsThisFrame = Mathf.FloorToInt(actualNormalizedTime);
                if (repsThisFrame > Repetitions) {
                    Repetitions = repsThisFrame;
                    Action();
                }
            }

            lastNormalizedTime = stateInfo.normalizedTime;
        }

        protected virtual void Action() {
            Gun.Controller.InputBuffer[State] = 0f;
        }
    }
}