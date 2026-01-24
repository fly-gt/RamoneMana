using DG.Tweening;
using UnityEngine;

public class ProgressView : MonoBehaviour {
    public SpriteRenderer target1;
    public SpriteRenderer target2;
    [Space]
    public SpriteRenderer progress1;
    public SpriteRenderer progress2;
    [Space]
    public float ownPosY;
    public float slidePosY = 6.5f;
    public float slideDuration = 0.5f;

    private void Awake() {
        ownPosY = transform.position.y;
        //Hide();
    }

    public void SetTarget(int value) {
        Set(value, target1, target2);
    }

    public void SetProgress(int value) {
        Set(value, progress1, progress2);
    }

    public void ToGame(bool game, bool force = false) {
        if (game) {
            if (force) {
                transform.SetY(ownPosY);
            } else {
                transform.DOMoveY(ownPosY, slideDuration).SetEase(Ease.OutBack, 0.5f);
            }
            return;
        }

        if (force) {
            transform.SetY(ownPosY + slidePosY);
        } else {
            transform.DOMoveY(ownPosY + slidePosY, slideDuration);
        }
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
