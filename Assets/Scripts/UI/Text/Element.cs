using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace UI.Text {
    internal abstract class Element {
        private static readonly Lazy<GameObject> prefab = new(() => Resources.Load<GameObject>("Prefabs/Text"));

        internal readonly GameObject instance;

        internal readonly Dictionary<string, Subpart> subparts;

        internal Element(ElementBuilder elementBuilder) {
            instance = Object.Instantiate(prefab.Value);

            subparts = new Dictionary<string, Subpart> {
                ["Text"] = new() {
                    rectTransform = instance.transform.Find("Text").GetComponent<RectTransform>(),
                    renderPriority = elementBuilder.textRenderPriority
                },
                ["Background"] = new() {
                    rectTransform = instance.transform.Find("Background").GetComponent<RectTransform>(),
                    renderPriority = elementBuilder.backgroundRenderPriority
                }
            };

            TextMeshProUGUI textMesh = subparts["Text"].rectTransform.GetComponent<TextMeshProUGUI>();
            Image backgroundImage = subparts["Background"].rectTransform.GetComponent<Image>();

            textMesh.text = elementBuilder.text;
            textMesh.rectTransform.sizeDelta = textMesh.GetPreferredValues();

            (textMesh.text == "").Then(() => textMesh.gameObject.SetActive(false));

            Vector2 sizeDelta = textMesh.rectTransform.sizeDelta + elementBuilder.backgroundSizeDelta;

            backgroundImage.gameObject.SetActive(elementBuilder.background);
            backgroundImage.rectTransform.sizeDelta = sizeDelta;
            backgroundImage.color = elementBuilder.backgroundColor;
        }

        internal Vector2 Size => subparts["Background"].rectTransform.sizeDelta;
        internal abstract Vector2 Render(Vector2 start, int direction, List<Element> elements);

        internal struct Subpart {
            internal RectTransform rectTransform;
            internal int renderPriority;
        }
    }

    internal sealed class TextField : Element {
        internal TextField(ElementBuilder builder) : base(builder) { }

        internal override Vector2 Render(Vector2 start, int direction, List<Element> elements) {
            start.x += Size.x / 2 * direction;
            subparts.Values.ForEach(subpart => subpart.rectTransform.anchoredPosition = start);

            start.x += Size.x / 2 * direction;
            return start;
        }
    }

    internal sealed class FullBackground : Element {
        internal FullBackground(ElementBuilder builder) : base(builder) { }

        internal override Vector2 Render(Vector2 start, int direction, List<Element> elements) {
            float x = elements.Select(element => element.Size.x).Sum();
            float y = elements.Select(element => element.Size.y).Max();

            subparts["Background"].rectTransform.anchoredPosition = new Vector2(x, y);

            return start;
        }
    }
}