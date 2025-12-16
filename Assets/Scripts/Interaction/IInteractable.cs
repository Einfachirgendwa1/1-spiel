namespace Interaction {
    internal interface IInteractable {
        public string Description { get; }
        public bool CanInteract { get; } // werden Variablen namen nicht klein geschrieben???

        public void Interact();
    }
}