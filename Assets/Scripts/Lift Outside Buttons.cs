using UnityEngine;

public class Lift0utsideButtons : MonoBehaviour, IInteractable
{
    LiftCabineButton cabine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cabine = GameObject.Find("Fahrstuhl_Knopf innen").GetComponent<LiftCabineButton>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact() {
        if (transform.name == "Fahrstuhl_Knopf unten") {
            Debug.Log("unten gedrückt");
            if (!cabine.isDown && !cabine.isRunning) {
                cabine.GoDown();               
            }

        } else if (transform.name == "Fahrstuhl_Knopf oben") {
            Debug.Log("oben gedrückt");
            if (cabine.isDown && !cabine.isRunning) {
                cabine.GoUp();
            }
        } 
    }
}
