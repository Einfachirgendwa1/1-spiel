using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace UI.Text {
    public class TextDisplay : MonoBehaviour {
        public HorizontalAlignmentOptions alignment;
        private readonly Dictionary<GameObject, int> render = new();

        private DisplayElement[] elements = { };
        private object[] rawLastElements = { };

        private RectTransform rectTransform => GetComponent<RectTransform>();

        internal void SetDisplayElements(params object[] newElements) {
            if (newElements.SequenceEqual(rawLastElements)) {
                return;
            }

            rawLastElements = newElements;
            elements = newElements
                .Select(obj => obj switch {
                    DisplayElement t => t,
                    KeyCode key      => new DisplayButton { key = key },
                    _                => new DisplayText { text = obj.ToString() }
                })
                .ToArray();

            foreach (GameObject go in render.Keys) {
                Destroy(go);
            }

            GameObject bgPrefab = Resources.Load<GameObject>("Prefabs/Text/Background");
            GameObject background = Instantiate(bgPrefab, transform);
            background.name = "Background";
            render.Add(background, -10);

            foreach (DisplayElement element in elements) {
                GameObject prefab = Resources.Load<GameObject>(element.Prefab);
                GameObject obj = Instantiate(prefab, transform);

                element.subparts = new Dictionary<string, GameObject>();
                foreach (KeyValuePair<string, int> kv in element.Subparts) {
                    GameObject subpart = obj.transform.Find(kv.Key).gameObject;
                    element.subparts.Add(kv.Key, subpart);

                    subpart.name = $"{element.GameObjectName ?? gameObject.name}.{kv.Key}";
                    subpart.transform.SetParent(transform);
                    render.Add(subpart, kv.Value);
                }

                render.Add(obj, 10);
                element.Start();
            }

            int direction = alignment == HorizontalAlignmentOptions.Right ? -1 : 1;

            Vector2 size = new();
            foreach (DisplayElement element in elements) {
                size.x += element.Size.x;
                size.y = Math.Max(size.y, element.Size.y);
            }

            float right = alignment switch {
                HorizontalAlignmentOptions.Left   => rectTransform.rect.xMin,
                HorizontalAlignmentOptions.Center => size.x / -2,
                HorizontalAlignmentOptions.Right  => rectTransform.rect.xMax,
                _                                 => throw new Exception($"Alignment not supported: {alignment}")
            };

            foreach (DisplayElement element in elements) {
                element.Move(new Vector2(right, rectTransform.anchoredPosition.y));
                right += element.Size.x * direction;
            }

            List<GameObject> sortedRender = render.OrderBy(kv => kv.Value).Select(kv => kv.Key).ToList();
            for (int i = 0; i < sortedRender.Count; i++) {
                sortedRender[i].transform.SetSiblingIndex(i);
            }

            Transform backgroundImage = background.transform.Find("Image");

            backgroundImage.GetComponent<RectTransform>().anchoredPosition = rectTransform.anchoredPosition;
            backgroundImage.GetComponent<RectTransform>().sizeDelta = size + new Vector2(40, 20);
        }

        internal void SetVisible(bool visible) {
            foreach (GameObject go in render.Keys.Where(go => go != null)) {
                go.SetActive(visible);
            }
        }
    }
}