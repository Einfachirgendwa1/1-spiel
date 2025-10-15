using UI.Menus;
using UnityEngine;

namespace Settings {
    public class SceneSettings : MonoBehaviour {
        public bool ShowCursor;

        private void Start() {
            Menu.Cursor(ShowCursor);
        }
    }
}