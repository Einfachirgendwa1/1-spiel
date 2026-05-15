using System;
using System.Collections;
using Interaction;
using UnityEngine;
using Validation;

/*
 * X und Y kooordinaten sind wegen dem impot aus blender komischs
 */
namespace Lift {
    public class LiftButton : MonoBehaviour, IInteractable {
        public enum Mode {
            Low,
            High,
            Toggle
        }

        [NonNull] public Lift lift;
        [NonNull] public Transform liftTransform;
        [Range(1f, 3f)]
        public int buttonType; //1 -> unten; 2->im lift;3->oben
        [NonNull] public Mode mode;

        
        

        public string Description =>
            mode switch {
                Mode.Toggle => $"move lift {(lift.isUp ? "down" : "up")}",
                _           => "call lift"
            };

        bool IInteractable.CanInteract => throw new NotImplementedException(); //WAS MACHT DAS ????

        public void Interact() {
           
            switch (buttonType) 
            {
                case 1:
                    lift.Move(false);
                    break;
                case 2:
                    if (lift.isUp)
                    {
                        lift.Move(false);
                    }else if (!lift.isUp)
                    {
                        lift.Move(true);
                    }
                    break;
                case 3:
                    lift.Move(true);
                    break;

            
            
            }
        }

      
        

        /*private bool IsUp() {
            return liftTransform.position.z >= lift.upPosY - lift.delta;
        }
        [SerializeField]
        private bool IsDown() {
            return liftTransform.position.z <= lift.downPosY + lift.delta;
        }

        private IEnumerator Move() {
            if (CanInteract) {
                Vector3 direction = IsUp() ? Vector3.down : Vector3.up;

                while (direction == Vector3.down ? !IsDown() : !IsUp()) {
                    liftTransform.position += direction * lift.speed;
                    yield return new WaitForEndOfFrame();
                }
            }

            yield return null;
        }
        */
    }
}