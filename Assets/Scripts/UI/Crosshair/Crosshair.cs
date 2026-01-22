using System;
using Guns;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Crosshair {
    public abstract class Crosshair : MonoBehaviour {
        public Image rect;
        public GunController gunController;

        private readonly Image[] children = {
            null,
            null,
            null,
            null
        };

        internal void Start() {
            for (int i = 0; i < children.Length; ++i) {
                children[i] = Instantiate(rect, transform);
                children[i].rectTransform.Rotate(0f, 0f, i * 90f);
            }
        }

        internal void PositionChildren(Vector2 movement) {
            for (int i = 0; i < children.Length; ++i) {
                children[i].rectTransform.localPosition = i switch {
                    0 => new Vector3(movement.x, movement.y, 0),
                    1 => new Vector3(movement.y, -movement.x, 0),
                    2 => new Vector3(-movement.x, -movement.y, 0),
                    3 => new Vector3(-movement.y, movement.x, 0),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }
    }
}