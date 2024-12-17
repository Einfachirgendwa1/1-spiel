using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugPrinterBehaviour : MonoBehaviour {
    [HideInInspector] public Dictionary<int, string> clientValueMap = new();

    int count = 0;
    TextMeshProUGUI textMeshPro;

    public DebugPrinter NewPrinter() => new(count++, this);

    private void Start() {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        string futureText = "";
        for (int index = 0; index < count; index++) {
            futureText += clientValueMap[index];
        }
        textMeshPro.text = futureText;
    }
}

public class DebugPrinter {
    readonly DebugPrinterBehaviour behaviour;
    readonly int index;

    public DebugPrinter(int index, DebugPrinterBehaviour behaviour) {
        this.index = index;
        this.behaviour = behaviour;
    }

    public void Print(string text) {
        behaviour.clientValueMap[index] = text;
    }
}