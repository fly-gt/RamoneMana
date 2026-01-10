using DG.Tweening;
using UnityEngine;

public class TutorialArrowUi : MonoBehaviour {
    public RectTransform rect;
    private Tween moveTween;
    private Vector3 point1, point2;
    private Sequence pulseSequence;
    public float scaleDown = 0.9f;
    public float duration = 0.3f;
    private Vector3 originalScale;

    private void Awake() {
        originalScale = transform.localScale;
    }

    public void SetShow(Vector3 p1, Vector3 p2) {
        gameObject.SetActive(true);
        point1 = p1;
        point2 = p2;
        StartPulseLoop();
        //rect.localPosition = p1;
        //MoveDown();
    }

    public void Disable() {
        gameObject.SetActive(false);
    }

    void StartPulseLoop() {
        PulseOnce();
    }

    void PulseOnce() {
        pulseSequence?.Kill();

        float waitTime = 1f;

        pulseSequence = DOTween.Sequence();

        pulseSequence.Append(transform.DOScale(originalScale * scaleDown, duration).SetEase(Ease.InOutQuad));
        pulseSequence.Append(transform.DOScale(originalScale, duration).SetEase(Ease.OutBack));
        pulseSequence.AppendInterval(waitTime);
        pulseSequence.OnComplete(PulseOnce);
    }


    //void MoveDown() {
    //    moveTween = transform.DOLocalMove(point2, 1f)
    //        .SetEase(Ease.InOutSine)
    //        .OnComplete(MoveUp);
    //}

    //void MoveUp() {
    //    moveTween = transform.DOLocalMove(point1, 1f)
    //        .SetEase(Ease.InOutSine)
    //        .OnComplete(MoveDown);
    //}

    private void OnDisable() {
        moveTween?.Kill();
    }
}
