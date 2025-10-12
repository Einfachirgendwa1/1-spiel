using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI {
    public class OnScreenDebugText : MonoBehaviour {
        internal static readonly List<Cursor> cursors = new();
        public TextMeshProUGUI textMesh;

        private void Update() {
            textMesh.text = "";
            foreach (Cursor cursor in cursors) {
                textMesh.text += $"{cursor.GetString()}\n";
            }
        }
    }

    internal class Cursor {
        internal string description;
        internal string name;

        internal Cursor() {
            OnScreenDebugText.cursors.Add(this);
        }

        internal string GetString() {
            string ret = "";

            if (name != "") {
                ret += $"[{name}] ";
            }

            ret += description;

            return ret;
        }
    }
}