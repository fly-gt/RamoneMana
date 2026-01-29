using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class UIButtonClick : MonoBehaviour, IPointerEnterHandler {
    public Button button;
    public RectTransform rectTransform;
    [Space]
    public bool ClickScale = true;
    public float clickScale = 0.7f;
    public float clickDuration = 0.3f;
    [Space]
    public AudioEventAsset clickAE;
    public AudioEventAsset enterAE;

    [ContextMenu("Click")]
    public void Click() {
        if (rectTransform) {
            if (ClickScale) {
                rectTransform.DOKill();

                Sequence clickSequence = DOTween.Sequence();
                clickSequence.Append(rectTransform.DOScale(Vector3.one * clickScale, clickDuration).SetEase(Ease.OutQuad));
                clickSequence.Append(rectTransform.DOScale(1f, clickDuration).SetEase(Ease.OutBounce));
            }
        }

        if (clickAE) {
            AudioManager.TryPlay(clickAE, new AudioPlayData {
                Position = Camera.main.transform.position
            });
        }

        foreach (var ef in GetComponents<ButtonEffect>()) {
            ef.Clear();
        }

        VibrationManager.Medium();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (enterAE) {
            AudioManager.TryPlay(enterAE, new AudioPlayData {
                Position = Camera.main.transform.position
            });
        }
    }
}
