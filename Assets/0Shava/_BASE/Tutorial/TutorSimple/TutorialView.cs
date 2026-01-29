using Cysharp.Threading.Tasks;
using DG.Tweening;
using NaughtyAttributes;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Utility;

public class TutorialView : MonoBehaviour {
    public TMP_Text messageTxt;
    public RectTransform messageRect;
    [Space]
    public Canvas canvas;
    public GraphicRaycaster raycaster;
    public CanvasGroup canvasGroup;
    public RectTransform mark;
    public RectTransform blackout;
    public Image blackoutImage;
    public Transform spawnPoint, standPoint;
    public MarkUi markUi;
    public RectTransform arrow;
    public TutorialArrowUi tutorialArrowUi;
    public AudioEventAsset bubbleShowAE, bubbleHideAE;

    public event Action OnMarkDown;

    private void Awake() {
        //canvasGroup.alpha = 0;
        raycaster.enabled = false;
        messageRect.gameObject.SetActive(false);
        arrow.gameObject.SetActive(false);
    }

    private void Start() {
        markUi.OnDown += MarkDown;
    }

    private void OnDestroy() {
        markUi.OnDown -= MarkDown;
    }

    private void MarkDown() {
        OnMarkDown?.Invoke();
    }

    public void SetGraphics(bool value) {
        raycaster.enabled = value;
    }

    public async UniTask ShowMessage(string message, Vector2 rectPos, Vector2 offsetRect = default, AnchorType anchorType = AnchorType.Default) {
        messageRect.gameObject.SetActive(true);
        Utility.SetAnchor(messageRect, anchorType);

        messageRect.localPosition = rectPos += offsetRect;
        messageTxt.text = message;

        messageRect.localScale = Vector3.zero;
        //AudioManager.Instance.TryPlay(bubbleShowAE, messageRect.transform.position);
        await messageRect.DOScale(Vector3.one, 0.2f);
    }

    public async UniTask HideMessage() {
        await messageRect.DOScale(Vector3.zero, 0.5f);
        messageRect.gameObject.SetActive(false);
    }

    public async UniTask ShowArrow(Vector2 localPos, Vector2 offsetP1 = default, Vector2 offsetP2 = default, AnchorType anchorType = AnchorType.Default) {
        var p1 = localPos + offsetP1;
        var p2 = localPos + offsetP2;

        arrow.gameObject.SetActive(true);
        Utility.SetAnchor(arrow.GetComponent<RectTransform>(), anchorType);
        arrow.localPosition = p1;
        arrow.localScale = Vector3.zero;
        await arrow.DOScale(Vector3.one, 0.2f);
        tutorialArrowUi.SetShow(p1, p2);
    }

    public void HideArrow() {
        tutorialArrowUi.Disable();
        arrow.gameObject.SetActive(false);
    }

    public void CloseMarkAndMesage() {

    }

    [Button]
    public void SetBlackout() {
        if (blackout) {
            blackout.anchoredPosition = -mark.anchoredPosition;
            blackout.sizeDelta = canvas.renderingDisplaySize;
        }
    }
}


