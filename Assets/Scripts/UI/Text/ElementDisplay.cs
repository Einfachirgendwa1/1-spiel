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

            int direction = alignment == HorizontalAlignmentOptions.Right ? -1 : 1;

            Vector2 size = new();
            foreach (Element element in elements) {
                size.x += element.Size().x;
                size.y = Math.Max(size.y, element.Size().y);
            }

            float right = alignment switch {
                HorizontalAlignmentOptions.Left   => rectTransform.rect.xMin,
                HorizontalAlignmentOptions.Center => size.x / -2,
                HorizontalAlignmentOptions.Right  => rectTransform.rect.xMax,
                _                                 => throw new Exception($"Alignment not supported: {alignment}")
            };

            foreach (Element element in elements) {
                float middle = element.Size().x / 2;
                element.Move(new Vector2(right + middle * direction, rectTransform.anchoredPosition.y));
                right += middle * 2 * direction;
            }

            Element background = new ElementBuilder(new Color(0, 0, 0, 0.8f)) {
                backgroundSizeDelta = size + new Vector2(40, 20),
                backgroundRenderPriority = -10
            }.Build();

            background.Move(rectTransform.anchoredPosition);
            elements.Add(background);

            foreach (Element element in elements) {
                foreach (Element.Subpart subpart in element.subparts.Values) {
                    Vector2 preserve = subpart.rectTransform.anchoredPosition;
                    subpart.rectTransform.transform.SetParent(transform, true);
                    subpart.rectTransform.anchoredPosition = preserve;
                    render.Add(subpart);
                }

                Destroy(element.instance);
            }

            alive
                .OrderBy(subpart => subpart.renderPriority)
                .ForEach((idx, subpart) => subpart.rectTransform.transform.SetSiblingIndex(idx));
        }

        internal void SetVisible(bool visible) {
            alive.ForEach(subpart => subpart.rectTransform.gameObject.SetActive(visible));
        }
    }
}