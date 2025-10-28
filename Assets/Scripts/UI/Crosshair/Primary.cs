using Guns;
using UnityEngine;

namespace UI.Crosshair {
    public class Primary : Crosshair {
        public float extensionSize;
        public float extensionSpeed;

        private float extension;

        private void Update() {
            float target = gunController.ActiveOrRequested(State.Unequip) switch {
                true  => 400,
                false => gunController.ActiveOrRequested(State.Shoot) ? extensionSize : 0
            };

            extension = Mathf.Lerp(extension, target, extensionSpeed);
            PositionChildren(new Vector2(0, extension + rect.rectTransform.rect.height / 2));
        }
    }
}