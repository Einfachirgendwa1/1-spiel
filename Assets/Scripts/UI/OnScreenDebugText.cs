using System.Collections.Generic;
using System.Linq;
using UI.Text;

namespace UI {
    public class OnScreenDebugText : ElementDisplay {
        internal static readonly List<Cursor> Cursors = new();

        private void Update() {
            if (Cursors.Count == 0) {
                SetElements(new ElementBuilder(""));
                return;
            }

            string res = Cursors.Select(c => c.Str).Aggregate((s1, s2) => $"{s1}\n{s2}");
            SetElements(new ElementBuilder(res));
        }
    }

    internal class Cursor {
        internal string Str;

        internal Cursor() {
            OnScreenDebugText.Cursors.Add(this);
        }
    }
}