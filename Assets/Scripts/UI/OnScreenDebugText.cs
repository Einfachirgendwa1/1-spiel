using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace UI {
    public class OnScreenDebugText : MonoBehaviour {
        internal static readonly List<Cursor> cursors = new();
        public TextMeshProUGUI textMesh;

        private void Update() {
            textMesh.text = "";
            foreach (string cursor in cursors.Select(c => c.str)) {
                textMesh.text += $"{cursor}\n";
            }
        }
    }

    internal class Cursor {
        internal string str;

        internal Cursor() => OnScreenDebugText.cursors.Add(this);
    }
}