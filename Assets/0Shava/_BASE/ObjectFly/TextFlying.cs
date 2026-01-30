using System;
using TMPro;
using UnityEngine;

public class TextFlying : ObjectFlying {
    private string text;
    private Color color;

    public void Fly(string text, Color color, Vector3 startPos, Vector3 endPos, Action completed = null) {
        this.text = text;
        this.color = color;
        Fly(startPos, endPos, completed);
    }

    public override void FlyBehaviour(GameObject gameObject) {
        var tmpText = gameObject.GetComponent<TMP_Text>();
        tmpText.color = color;
        tmpText.text = text;
    }
}
