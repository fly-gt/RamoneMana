using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RootLoader : MonoBehaviour {
    public Image loadingImage;
    public TMP_Text progressText;
    private Tween tween;

    private void OnEnable() {
        loadingImage.fillAmount = 0f;
    }

    public void SetProgress(float current, float max) {
        //Debug.Log($"{current} {max}");

        tween?.Kill();

        if (max <= 0f) {
            loadingImage.fillAmount = 0f;

            if (progressText) {
                progressText.SetText("{0}%", 0);
            }

            return;
        }

        current = Mathf.Clamp(current, 0f, max);

        float normalized = current / max;
        tween = loadingImage.DOFillAmount(normalized, 0.1f);

        if (progressText) {
            int percent = Mathf.RoundToInt(normalized * 100f);
            progressText.SetText("{0}%", percent);
        }
    }

    private void OnDisable() {
        tween?.Kill();
    }
}
