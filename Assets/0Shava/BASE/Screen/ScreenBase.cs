using Cysharp.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class ScreenBase : MonoBehaviour {
    [SerializeField] private CanvasGroup canvasGroup;
    private CanvasGroupFade canvasGroupFade;

    private void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void Initialize() {
        canvasGroupFade = new CanvasGroupFade(canvasGroup);
        canvasGroup.alpha = 0;
    }

    public virtual async UniTask Render(object ctx = null, bool fade = true) {
        await canvasGroupFade.SetWithFade(true);
    }

    public async UniTask Close(bool fade = true) {
        await canvasGroupFade.SetWithFade(false);
    }
}
