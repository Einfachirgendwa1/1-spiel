using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace UI.Text {
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global")]
    internal record ElementBuilder {
        internal bool Background = true;
        internal Color BackgroundColor;
        internal int BackgroundRenderPriority = -1;
        internal Vector2 BackgroundSizeDelta;
        private Mode mode = Mode.TextField;
        internal string Text;
        internal int TextRenderPriority = 0;

        internal ElementBuilder(string text) {
            Text = text;
            Background = false;
        }

        internal ElementBuilder(Color color, string text = "", bool fullBackground = false) {
            fullBackground.Then(() => mode = Mode.FullBackground);

            Text = text;
            BackgroundColor = color;
        }

        internal ElementBuilder(KeyCode key) {
            Text = key.ToString();
            BackgroundColor = new Color(0, 0.7f, 1, 0.9f);
            BackgroundSizeDelta = new Vector2(20, 10);
        }

        internal Element Build() {
            return mode switch {
                Mode.TextField      => new TextField(this),
                Mode.FullBackground => new FullBackground(this),
                _                   => throw new ArgumentOutOfRangeException()
            };
        }

        private enum Mode {
            TextField,
            FullBackground
        }
    }
}