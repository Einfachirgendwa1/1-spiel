using Unity.VisualScripting;
using UnityEngine;

namespace Interaction {
    public class DoorOpen : MonoBehaviour, IInteractable {
        
        [SerializeField]
        private bool isOpen;
        [SerializeField]
        private bool leftWing;
        [Range(-1, 1)]
        private int turnMultiplier;

        private GameObject pivot; 

        public void Start()
        {
            pivot = this.transform.parent.gameObject;

            if (leftWing)
            {
                turnMultiplier = -1;
            }
            else
            {
                turnMultiplier = 1; 
            }
        }
        public string Description => "move door";

        public bool CanInteract => true;

        public void Interact() {
            if (!isOpen) {
                pivot.transform.Rotate(0, 0, turnMultiplier * 90);
                isOpen = true;
            } else {
                pivot.transform.Rotate(0, 0, turnMultiplier * -90);
                isOpen = false;
            }
        }  
    }
}