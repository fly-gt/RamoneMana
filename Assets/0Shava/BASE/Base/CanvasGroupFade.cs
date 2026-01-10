using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class CanvasGroupFade {
    private readonly CanvasGroup canvasGroup;
    private float fadeDuration = 0.2f;
    public CanvasGroupFade(CanvasGroup canvasGroup) {
        this.canvasGroup = canvasGroup;
        canvasGroup.gameObject.SetActive(false);
    }

    public async UniTask SetWithFade(bool active) {
        if (active) {
            canvasGroup.gameObject.SetActive(true);
            //canvasGroup.interactable = false;

            await canvasGroup.DOFade(1, fadeDuration)
                .SetEase(Ease.Linear)
                .SetUpdate(true)
                .OnComplete(() => {
                    //canvasGroup.interactable = true;
                });

            return;
        }

        await canvasGroup.DOFade(0, fadeDuration)
            .SetEase(Ease.Linear)
            .SetUpdate(true)
            .OnComplete(() => {
                canvasGroup.gameObject.SetActive(false);
            });
    }

    public void Set(bool active) {
        if (active) {
            canvasGroup.gameObject.SetActive(true);
            canvasGroup.alpha = 1;

            return;
        }

        canvasGroup.alpha = 0;
        canvasGroup.gameObject.SetActive(false);
    }
}

