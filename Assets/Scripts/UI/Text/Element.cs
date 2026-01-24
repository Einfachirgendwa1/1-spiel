using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace UI.Text {
    internal abstract class Element {
        private static readonly Lazy<GameObject> Prefab = new(() => Resources.Load<GameObject>("Prefabs/Text"));

        internal readonly GameObject Instance;

        internal readonly Dictionary<string, Subpart> Subparts;

        internal Element(ElementBuilder elementBuilder) {
            Instance = Object.Instantiate(Prefab.Value);

            Subparts = new Dictionary<string, Subpart> {
                ["Text"] = new() {
                    RectTransform = Instance.transform.Find("Text").GetComponent<RectTransform>(),
                    RenderPriority = elementBuilder.TextRenderPriority
                },
                ["Background"] = new() {
                    RectTransform = Instance.transform.Find("Background").GetComponent<RectTransform>(),
                    RenderPriority = elementBuilder.BackgroundRenderPriority
                }
            };

            TextMeshProUGUI textMesh = Subparts["Text"].RectTransform.GetComponent<TextMeshProUGUI>();
            Image backgroundImage = Subparts["Background"].RectTransform.GetComponent<Image>();

            textMesh.text = elementBuilder.Text;
            textMesh.rectTransform.sizeDelta = textMesh.GetPreferredValues();

            (textMesh.text == "").Then(() => textMesh.gameObject.SetActive(false));

            Vector2 sizeDelta = textMesh.rectTransform.sizeDelta + elementBuilder.BackgroundSizeDelta;

            backgroundImage.gameObject.SetActive(elementBuilder.Background);
            backgroundImage.rectTransform.sizeDelta = sizeDelta;
            backgroundImage.color = elementBuilder.BackgroundColor;
        }

        internal Vector2 Size => Subparts["Background"].RectTransform.sizeDelta;
        internal abstract Vector2 Render(Vector2 start, int direction, List<Element> elements);

        internal struct Subpart {
            internal RectTransform RectTransform;
            internal int RenderPriority;
        }
    }

    internal sealed class TextField : Element {
        internal TextField(ElementBuilder builder) : base(builder) { }

        internal override Vector2 Render(Vector2 start, int direction, List<Element> elements) {
            start.x += Size.x / 2 * direction;
            Subparts.Values.ForEach(subpart => subpart.RectTransform.anchoredPosition = start);

            start.x += Size.x / 2 * direction;
            return start;
        }
    }

    internal sealed class FullBackground : Element {
        internal FullBackground(ElementBuilder builder) : base(builder) { }

        internal override Vector2 Render(Vector2 start, int direction, List<Element> elements) {
            float x = elements.Select(element => element.Size.x).Sum();
            float y = elements.Select(element => element.Size.y).Max();

            Subparts["Background"].RectTransform.anchoredPosition = new Vector2(x, y);

            return start;
        }
    }
}