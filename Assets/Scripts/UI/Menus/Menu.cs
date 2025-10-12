using UnityEngine;

namespace UI.Menus {
    internal static class Menu {
        internal static void Cursor(bool visible) {
            UnityEngine.Cursor.visible = visible;
            UnityEngine.Cursor.lockState = visible ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
}