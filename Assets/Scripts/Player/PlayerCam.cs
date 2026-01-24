using UnityEngine;
using static Settings.Global.Input.Mouse;

namespace Player {
    public class PlayerCam : MonoBehaviour {
        internal static bool Paused = false;

        public Transform player;
        public Transform cameraHolder;

        private float xRotation;
        private float yRotation;

        private void Update() {
            if (Paused) {
                return;
            }

            yRotation += Input.GetAxisRaw("Mouse X") * SensitivityX;
            xRotation -= Input.GetAxisRaw("Mouse Y") * SensitivityY;

            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            player.rotation = Quaternion.Euler(0, yRotation, 0);
            cameraHolder.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        }
    }
}