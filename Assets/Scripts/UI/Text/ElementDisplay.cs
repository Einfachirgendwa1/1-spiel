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

        private IEnumerable<Element.Subpart> Alive => render.Where(subpart => subpart.RectTransform != null);
        private RectTransform RectTransform => GetComponent<RectTransform>();

        internal void SetElements(params ElementBuilder[] newElements) {
            if (newElements.SequenceEqual(rawLastElements)) {
                return;
            }

            Alive.ForEach(subpart => Destroy(subpart.RectTransform.gameObject));

            rawLastElements = newElements;
            elements = newElements.Select(builder => builder.Build()).ToList();

            foreach (Element element in elements) {
                foreach (Element.Subpart subpart in element.Subparts.Values) {
                    if (subpart.RectTransform.gameObject.activeSelf) {
                        subpart.RectTransform.transform.SetParent(transform);
                        render.Add(subpart);
                    } else {
                        Destroy(subpart.RectTransform.gameObject);
                    }
                }

                Destroy(element.Instance);
            }

            int direction = alignment == HorizontalAlignmentOptions.Right ? -1 : 1;

            float right = alignment switch {
                HorizontalAlignmentOptions.Left   => RectTransform.rect.xMin,
                HorizontalAlignmentOptions.Center => RectTransform.rect.x,
                HorizontalAlignmentOptions.Right  => RectTransform.rect.xMax,
                _                                 => throw new Exception($"Alignment not supported: {alignment}")
            };

            Vector2 start = new(right, RectTransform.anchoredPosition.y);
            elements.Aggregate(start, (vector, element) => element.Render(vector, direction, elements));

            Alive
                .OrderBy(subpart => subpart.RenderPriority)
                .ForEach((idx, subpart) => subpart.RectTransform.transform.SetSiblingIndex(idx));
        }

        internal void SetVisible(bool visible) {
            Alive.ForEach(subpart => subpart.RectTransform.gameObject.SetActive(visible));
        }
    }
}