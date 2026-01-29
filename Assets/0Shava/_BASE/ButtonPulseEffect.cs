using UnityEngine;
using DG.Tweening;

public class ButtonEffect : MonoBehaviour {
    public virtual void Clear() {

    }
}

public class ButtonPulseEffect : ButtonEffect {
    [Header("Настройки пульсации")]
    public float scaleDown = 0.9f;           // Насколько уменьшается кнопка
    public float duration = 0.15f;           // Скорость пульсации
    public float interval = 2f;              // Интервал между пульсами
    public bool randomizeInterval = true;    // Делать ли случайный интервал

    private Vector3 originalScale;
    private Sequence pulseSequence;
    private bool initialized = false;

    void OnEnable() {
        // Безопасно подождём 1 кадр, чтобы UI успел просчитаться
        StartCoroutine(InitAndStart());
    }

    System.Collections.IEnumerator InitAndStart() {
        yield return null; // ждём 1 кадр

        if (!initialized) {
            originalScale = transform.localScale;
            initialized = true;
        }

        StartPulseLoop();
    }

    void StartPulseLoop() {
        PulseOnce();
    }

    void PulseOnce() {
        pulseSequence?.Kill();

        float waitTime = randomizeInterval
            ? Random.Range(interval * 0.5f, interval * 1.5f)
            : interval;

        pulseSequence = DOTween.Sequence();

        pulseSequence.Append(transform.DOScale(originalScale * scaleDown, duration).SetEase(Ease.InOutQuad));
        pulseSequence.Append(transform.DOScale(originalScale, duration).SetEase(Ease.OutBack));
        pulseSequence.AppendInterval(waitTime);
        pulseSequence.OnComplete(PulseOnce);
    }

    public override void Clear() {
        pulseSequence?.Kill();
    }

    void OnDisable() {
        if (pulseSequence != null && pulseSequence.IsActive()) {
            pulseSequence.Kill();
            pulseSequence = null;
        }

        if (initialized)
            transform.localScale = originalScale;
    }
}
