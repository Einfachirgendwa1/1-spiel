using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace UI.Text {
    public class ElementDisplay : MonoBehaviour {
        public HorizontalAlignmentOptions alignment;
        private readonly List<Element.Subpart> render = new();

        private List<Element> elements = new();
        private ElementBuilder[] rawLastElements = { };

        private IEnumerable<Element.Subpart> alive => render.Where(subpart => subpart.rectTransform != null);
        private RectTransform rectTransform => GetComponent<RectTransform>();

        internal void SetElements(params ElementBuilder[] newElements) {
            if (newElements.SequenceEqual(rawLastElements)) {
                return;
            }

            alive.ForEach(subpart => Destroy(subpart.rectTransform.gameObject));

            rawLastElements = newElements;
            elements = newElements.Select(builder => builder.Build()).ToList();

            foreach (Element element in elements) {
                foreach (Element.Subpart subpart in element.subparts.Values) {
                    if (subpart.rectTransform.gameObject.activeSelf) {
                        subpart.rectTransform.transform.SetParent(transform);
                        render.Add(subpart);
                    } else {
                        Destroy(subpart.rectTransform.gameObject);
                    }
                }

                Destroy(element.instance);
            }

            int direction = alignment == HorizontalAlignmentOptions.Right ? -1 : 1;

            float right = alignment switch {
                HorizontalAlignmentOptions.Left   => rectTransform.rect.xMin,
                HorizontalAlignmentOptions.Center => rectTransform.rect.x,
                HorizontalAlignmentOptions.Right  => rectTransform.rect.xMax,
                _                                 => throw new Exception($"Alignment not supported: {alignment}")
            };

            Vector2 start = new(right, rectTransform.anchoredPosition.y);
            elements.Aggregate(start, (vector, element) => element.Render(vector, direction, elements));

            alive
                .OrderBy(subpart => subpart.renderPriority)
                .ForEach((idx, subpart) => subpart.rectTransform.transform.SetSiblingIndex(idx));
        }

        internal void SetVisible(bool visible) {
            alive.ForEach(subpart => subpart.rectTransform.gameObject.SetActive(visible));
        }
    }
}