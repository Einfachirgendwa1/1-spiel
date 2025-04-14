using UnityEngine;

public class AmmoChest : MonoBehaviour, IInteractable {
    public int ammunitionInChest = 20;
    private bool hasInteracted;
    private PlayerInventory playerInventory;

    private void Start() {
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
    }

    public void Interact() {
        if (!hasInteracted) {
            Debug.Log("chest interaction");
            playerInventory.amunition += ammunitionInChest;
            hasInteracted = true;
        }
    }
}