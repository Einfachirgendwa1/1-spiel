using Guns;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Crosshair {
    public class Secondary : Crosshair {
        [FormerlySerializedAs("DefaultExtension")]
        public float defaultExtension;

        [FormerlySerializedAs("ExtensionSize")]
        public float extensionSize;

        [FormerlySerializedAs("ExtensionSpeed")]
        public float extensionSpeed;

        private float extension;

        private void Update() {
            float target = gunController.ActiveOrRequested(State.Reload) ? extensionSize : 0f;
            extension = Mathf.Lerp(extension, target, extensionSpeed);

            float t = defaultExtension + extension + rect.rectTransform.rect.height / 2;
            PositionChildren(t * new Vector2(1, 1));
        }
    }
}