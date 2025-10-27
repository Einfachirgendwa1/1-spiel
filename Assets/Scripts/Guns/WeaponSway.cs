using UnityEngine;

namespace Guns {
    public class WeaponSway : MonoBehaviour {
        public float smooth;
        public float multiplier;

        private void Update() {
            float mouseX = Input.GetAxisRaw("Mouse X") * multiplier;
            float mouseY = Input.GetAxisRaw("Mouse Y") * multiplier;

            Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
            Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

            Quaternion targetRotation = rotationX * rotationY;

            float t = smooth * Time.deltaTime;
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, t);
        }
    }
}