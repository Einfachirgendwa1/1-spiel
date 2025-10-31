using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI.Text {
    internal abstract class DisplayElement {
        internal Dictionary<string, GameObject> subparts;

        internal abstract string Prefab { get; }
        internal abstract Dictionary<string, int> Subparts { get; }
        internal abstract Vector2 Size { get; }

        internal virtual string GameObjectName => null;

        internal abstract void Start();
        internal abstract void Move(Vector2 pos);
    }

    internal class DisplayText : DisplayElement {
        internal string text;
        protected TextMeshProUGUI textMesh;

        internal override string Prefab => "Prefabs/Text/DisplayText";
        internal override Dictionary<string, int> Subparts => new() { ["Text"] = 0 };
        internal override Vector2 Size => textMesh.rectTransform.sizeDelta;

        internal override string GameObjectName => $"\"{text}\"";

        internal override void Move(Vector2 pos) {
            pos.x += textMesh.rectTransform.sizeDelta.x / 2;
            textMesh.rectTransform.anchoredPosition = pos;
        }

        internal override void Start() {
            textMesh = subparts["Text"].GetComponent<TextMeshProUGUI>();
            textMesh.text = text;
            textMesh.rectTransform.sizeDelta = textMesh.GetPreferredValues();
            Debug.Log($"{text} {textMesh.rectTransform.sizeDelta}");
        }
    }

    internal class DisplayButton : DisplayText {
        private const float width = 20;

        internal KeyCode key;
        private RectTransform rectTransform;

        internal override string Prefab => "Prefabs/Text/DisplayButton";
        internal override Dictionary<string, int> Subparts => new() { ["Text"] = 0, ["Background"] = -1 };
        internal override Vector2 Size => rectTransform.sizeDelta;

        internal override string GameObjectName => $"Button \"{key.ToString()}\"";

        internal override void Start() {
            text = key.ToString();
            base.Start();

            rectTransform = subparts["Background"].GetComponent<RectTransform>();
            rectTransform.sizeDelta = textMesh.rectTransform.sizeDelta + new Vector2(width, 10);
        }

        internal override void Move(Vector2 pos) {
            pos.x += width / 2;
            base.Move(pos);
            rectTransform.anchoredPosition = textMesh.rectTransform.anchoredPosition;
        }
    }
}