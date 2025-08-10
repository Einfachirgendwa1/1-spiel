using UnityEngine;

public class LiftCabineButton : MonoBehaviour, IInteractable
{
    public bool isDown;
    public bool isRunning;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (transform.position.y < 1) {
            isDown = true;
        }
        else {
            isDown = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact() {
        Debug.Log("innen gedrückt");
        if (isDown && !isRunning) {
                GoUp();
            }
            else if (!isDown && !isRunning) {
                GoDown();
            }
    }

        public void GoUp() {
            isDown = false;
            isRunning = true;
            while (transform.position.y < 3) {
                transform.Translate(Vector3.up * 2 * Time.deltaTime);
            }
            isRunning = false;
        }

        public void GoDown() {
            isDown = true;
            isRunning = true;
            while (transform.position.y > 0.6f) {
                transform.Translate(Vector3.down * 2 * Time.deltaTime);
            }
            isRunning = false;
        }
    }
