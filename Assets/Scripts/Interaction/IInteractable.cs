namespace Interaction {
    internal interface IInteractable {
        public string Description { get; }
        public bool CanInteract { get; }

        public void Interact();
    }
}