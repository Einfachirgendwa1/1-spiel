using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace UI.Text {
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global")]
    internal record ElementBuilder {
        internal bool background = true;
        internal Color backgroundColor;
        internal int backgroundRenderPriority = -1;
        internal Vector2 backgroundSizeDelta;
        internal string text;
        internal int textRenderPriority = 0;

        internal ElementBuilder(string text) {
            this.text = text;
            background = false;
        }

        internal ElementBuilder(Color color, string text = "") {
            this.text = text;
            backgroundColor = color;
        }

        internal ElementBuilder(KeyCode key) {
            text = key.ToString();
            backgroundColor = new Color(0, 0.7f, 1, 0.9f);
            backgroundSizeDelta = new Vector2(20, 10);
        }

        internal Element Build() => new(this);
    }
}