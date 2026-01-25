using UI.Menus;
using UnityEngine;

namespace Settings {
    public class SceneSettings : MonoBehaviour {
        public bool showCursor;

        private void Start() {
            Menu.Cursor(showCursor);
        }
    }
}