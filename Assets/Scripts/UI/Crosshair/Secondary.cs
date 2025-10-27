using Guns;
using UnityEngine;

namespace UI.Crosshair {
    public class Secondary : Crosshair {
        public float DefaultExtension;
        public float ExtensionSize;
        public float ExtensionSpeed;

        private float extension;

        private void Update() {
            float target = gunController.ActiveOrRequested(State.Reload) ? ExtensionSize : 0f;
            extension = Mathf.Lerp(extension, target, ExtensionSpeed);

            float t = DefaultExtension + extension + rect.rectTransform.rect.height / 2;
            PositionChildren(t * new Vector2(1, 1));
        }
    }
}