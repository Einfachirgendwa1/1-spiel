using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Printer : MonoBehaviour {
    [HideInInspector] public static Printer behaviour;
    [HideInInspector] public Dictionary<int, string> clientValueMap = new();

    static int count = 0;
    TextMeshProUGUI textMeshPro;

    public static DebugPrinter NewPrinter() => new(count++, behaviour);

    private void Start() {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        behaviour = this;
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
    readonly Printer behaviour;
    readonly int index;

    public DebugPrinter(int index, Printer behaviour) {
        this.index = index;
        this.behaviour = behaviour;
    }

    public void Print(string text) {
        behaviour.clientValueMap[index] = text;
    }
}