using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundPopupHandler : MonoBehaviour {
    public Image background;
    public float alpha = 0.7f;
    [Space]
    public bool rendered;

    private void Awake() {
        background.gameObject.SetActive(false);
    }

    public async UniTask Render() {
        if (rendered) {
            return;
        }

        rendered = true;
        background.SetAlpha(0);
        background.gameObject.SetActive(true);
        await background.DOFade(alpha, PopupManager.Instance.asset.FadeOnDuration).SetUpdate(true);
    }

    public async UniTask Close() {
        if (!rendered) {
            return;
        }

        await background.DOFade(0, PopupManager.Instance.asset.FadeOffDuration).SetUpdate(true).OnComplete(() => {
            background.gameObject.SetActive(false);
            rendered = false;
        });
    }

    public void CloseForce() {
        background.gameObject.SetActive(false);
        rendered = false;
    }
}
