using UnityEngine;
using static Settings.Input.Mouse;

namespace Player {
    public class PlayerCam : MonoBehaviour {
        public Transform player;
        public Transform cameraHolder;

        private float xRotation;
        private float yRotation;

        private void Update() {
            yRotation += Input.GetAxisRaw("Mouse X") * sensitivityX;
            xRotation -= Input.GetAxisRaw("Mouse Y") * sensitivityY;

            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            player.rotation = Quaternion.Euler(0, yRotation, 0);
            cameraHolder.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        }
    }
}