using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace UI.Text {
    internal sealed class Element {
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

            Vector2 sizeDelta = textMesh.rectTransform.sizeDelta + elementBuilder.backgroundSizeDelta;

            backgroundImage.gameObject.SetActive(elementBuilder.background);
            backgroundImage.rectTransform.sizeDelta = sizeDelta;
            backgroundImage.color = elementBuilder.backgroundColor;
        }

        internal void Move(Vector2 pos) {
            foreach (RectTransform rectTransform in subparts.Values.Select(subpart => subpart.rectTransform)) {
                rectTransform.anchoredPosition = pos;
            }
        }

        internal Vector2 Size() {
            float maxWidth = subparts.Values.Max(subpart => subpart.rectTransform.sizeDelta.x);
            float maxHeight = subparts.Values.Max(subpart => subpart.rectTransform.sizeDelta.y);
            return new Vector2(maxWidth, maxHeight);
        }

        internal struct Subpart {
            internal RectTransform rectTransform;
            internal int renderPriority;
        }
    }
}