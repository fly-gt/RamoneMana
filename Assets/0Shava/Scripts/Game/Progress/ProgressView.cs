using System.Collections;
using UnityEngine;

public class ProgressView : MonoBehaviour {
    public SpriteRenderer target1;
    public SpriteRenderer target2;
    [Space]
    public SpriteRenderer progress1;
    public SpriteRenderer progress2;

    private void Awake() {
        Hide();
    }

    public void SetTarget(int value) {
        Set(value, target1, target2);
    }

    public void SetProgress(int value) {
        Set(value, progress1, progress2);
    }

    private void Set(int value, SpriteRenderer s1, SpriteRenderer s2) {
        if (value < 0) {
            Debug.Log($"Set Error: {value}");
            return;
        }

        if (value <= 9) {
            s1.sprite = AppShared.Instance.settings.progressNumberData[value].Sprite;
            s2.sprite = null;
        } else {
            int firstDigit = value / 10;
            int secondDigit = value % 10;
            s1.sprite = AppShared.Instance.settings.progressNumberData[firstDigit].Sprite;
            s2.sprite = AppShared.Instance.settings.progressNumberData[secondDigit].Sprite;
        }
    }

    public void Hide() {
        target1.sprite = null;
        target2.sprite = null;
        progress1.sprite = null;
        progress2.sprite = null;
    }
}
