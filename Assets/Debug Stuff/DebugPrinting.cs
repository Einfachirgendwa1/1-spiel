using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Debug_Stuff {
    public class Printer : MonoBehaviour {
        public static Printer Behaviour;

        private static int count;
        public readonly Dictionary<int, string> ClientValueMap = new();
        private TextMeshProUGUI textMeshPro;

        private void Start() {
            textMeshPro = GetComponent<TextMeshProUGUI>();
            Behaviour = this;
        }

        private void Update() {
            string futureText = "";
            for (int index = 0; index < count; index++) {
                futureText += ClientValueMap[index];
            }

            textMeshPro.text = futureText;
        }

        public static DebugPrinter NewPrinter() {
            return new DebugPrinter(count++, Behaviour);
        }
    }

    public class DebugPrinter {
        private readonly Printer behaviour;
        private readonly int index;

        public DebugPrinter(int index, Printer behaviour) {
            this.index = index;
            this.behaviour = behaviour;
        }

        public void Print(string text) {
            behaviour.ClientValueMap[index] = text;
        }
    }
}