using UI.Menus;
using UnityEngine;
using UnityEngine.Serialization;

namespace Settings {
    public class SceneSettings : MonoBehaviour {
        [FormerlySerializedAs("ShowCursor")] public bool showCursor;

        private void Start() {
            Menu.Cursor(showCursor);
        }
    }
}