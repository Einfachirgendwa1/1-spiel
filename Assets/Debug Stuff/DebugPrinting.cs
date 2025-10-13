using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Printer : MonoBehaviour {
    public static Printer behaviour;

    private static int count;
    public Dictionary<int, string> clientValueMap = new();
    private TextMeshProUGUI textMeshPro;

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

    public static DebugPrinter NewPrinter() => new(count++, behaviour);
}

public class DebugPrinter {
    private readonly Printer behaviour;
    private readonly int index;

    public DebugPrinter(int index, Printer behaviour) {
        this.index = index;
        this.behaviour = behaviour;
    }

    public void Print(string text) {
        behaviour.clientValueMap[index] = text;
    }
}