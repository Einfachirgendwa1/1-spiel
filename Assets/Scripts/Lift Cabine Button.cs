using System.Threading.Tasks;
using UnityEngine;

public class LiftCabineButton : MonoBehaviour, IInteractable {
    [SerializeField] float speed = 0.2f;
    [SerializeField] float downPosY = 0.6f;
    [SerializeField] float upPosY = 3f;
    [SerializeField] float endValue = 0.008f;

    public bool isDown;
    public bool isRunning;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        if (transform.position.y < 1) {
            isDown = true;
        }
        else {
            isDown = false;
        }
    }

    // Update is called once per frame
    void Update() {

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

    async public void GoUp() {
        isDown = false;
        isRunning = true;
        Vector3 upPos = new Vector3(transform.position.x, upPosY, transform.position.z);
        while (transform.position != upPos) {
            //transform.Translate(Vector3.up * 2 * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, upPos, speed);
            if (Mathf.Abs(transform.position.y - upPos.y) < endValue) {
                transform.position = upPos;
            }
            await Task.Delay(10);
        }
        isRunning = false;
    }

    async public void GoDown() {
        isDown = true;
        isRunning = true;
        Vector3 downPos = new Vector3(transform.position.x, downPosY, transform.position.z);
        while (transform.position != downPos) {
            //transform.Translate(Vector3.up * 2 * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, downPos, speed);
            if (Mathf.Abs(transform.position.y - downPos.y) < endValue) {
                transform.position = downPos;
            }
            await Task.Delay(10);
        }
        isRunning = false;
    }
}
