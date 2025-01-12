using UnityEngine;

public class AmmoChest : MonoBehaviour, IInteractable
{
    public int ammunitionInChest = 20;
    PlayerInventory playerInventory;

    void Start() 
    {
        playerInventory = GameObject.Find("Player").GetComponent<PlayerInventory>();
    }
 public void Interact()
    {
        Debug.Log("chest interaction");
        playerInventory.amunition += ammunitionInChest;
    }
}
