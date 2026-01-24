using System;
using System.Collections.Generic;
using UnityEngine;

namespace Settings.Global.Input {
    internal static class Keybinds {
        internal static readonly Dictionary<Action, KeyCode> Keybindings = new() {
            [Action.Forward] = KeyCode.W,
            [Action.Backward] = KeyCode.S,
            [Action.Left] = KeyCode.A,
            [Action.Right] = KeyCode.D,
            [Action.Jump] = KeyCode.Space,
            [Action.Shoot] = KeyCode.Mouse0,
            [Action.Reload] = KeyCode.R,
            [Action.Interact] = KeyCode.E
        };

        internal static readonly Dictionary<int, KeyCode> WeaponSelect = new() {
            [0] = KeyCode.Alpha0,
            [1] = KeyCode.Alpha1,
            [2] = KeyCode.Alpha2,
            [3] = KeyCode.Alpha3,
            [4] = KeyCode.Alpha4,
            [5] = KeyCode.Alpha5,
            [6] = KeyCode.Alpha6,
            [7] = KeyCode.Alpha7,
            [8] = KeyCode.Alpha8,
            [9] = KeyCode.Alpha9
        };

        internal static bool Is(this Action action, Func<KeyCode, bool> f) {
            return Keybindings.TryGetValue(action, out KeyCode key) && f(key);
        }

        internal static bool Is(this int weapon, Func<KeyCode, bool> f) {
            return WeaponSelect.TryGetValue(weapon, out KeyCode key) && f(key);
        }
    }

    internal enum Action {
        Forward,
        Backward,
        Left,
        Right,
        Jump,
        Shoot,
        Reload,
        Interact
    }
}