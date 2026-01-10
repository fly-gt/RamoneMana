using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

[RequireComponent (typeof(CanvasGroup))]
public abstract class BasePopup : MonoBehaviour {
    public PopupFadeType fadeType = PopupFadeType.SimpleFade;
    public Transform root;
    public CanvasGroup canvasGroup;

    private void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual async UniTask Render(object ctx = null) {
        switch (fadeType) {
            case PopupFadeType.Default:
                root.gameObject.SetActive(true);
                break;
            case PopupFadeType.SimpleFade:
                await FadeOn();
                break;
            default:
                await UniTask.CompletedTask;
                break;
        }
    }

    public virtual async UniTask Close() {
        switch (fadeType) {
            case PopupFadeType.Default:
                root.gameObject.SetActive(false);
                break;
            case PopupFadeType.SimpleFade:
                await FadeOff();
                break;
            default:
                await UniTask.CompletedTask;
                break;
        }
    }

    private async UniTask FadeOn() {
        var fadeDuration = PopupManager.Instance.asset.FadeOnDuration;

        root.gameObject.SetActive(true);
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0f;

        await canvasGroup
            .DOFade(1, fadeDuration)
            .SetUpdate(true)
            .ToUniTask();

        canvasGroup.blocksRaycasts = true;
    }

    private async UniTask FadeOff() {
        var fadeDuration = PopupManager.Instance.asset.FadeOffDuration;

        canvasGroup.blocksRaycasts = false;

        await canvasGroup
            .DOFade(0, fadeDuration)
            .SetUpdate(true)
            .ToUniTask();

        root.gameObject.SetActive(false);
    }
}

public enum PopupFadeType {
    Default,
    SimpleFade
}
