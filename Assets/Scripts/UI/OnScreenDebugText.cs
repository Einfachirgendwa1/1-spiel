using System.Collections.Generic;
using System.Linq;
using UI.Text;

namespace UI {
    public class OnScreenDebugText : ElementDisplay {
        internal static readonly List<Cursor> cursors = new();

        private void Update() {
            string res = cursors.Select(c => c.str).Aggregate((s1, s2) => $"{s1}\n{s2}");
            SetElements(new ElementBuilder(res));
        }
    }

    internal class Cursor {
        internal string str;

        internal Cursor() => OnScreenDebugText.cursors.Add(this);
    }
}