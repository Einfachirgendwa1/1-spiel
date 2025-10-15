using UnityEditor;

namespace Editor {
    public class CloseWindowShortcut : UnityEditor.Editor {
        [MenuItem("Shortcuts/CloseWindowTab")]
        private static void CloseWindowTab() {
            EditorWindow.focusedWindow?.Close();
        }
    }
}