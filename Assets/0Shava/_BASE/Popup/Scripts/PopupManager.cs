using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BackgroundPopupHandler))]
public class PopupManager : Singletone<PopupManager> {
    public Canvas canvas;
    public PopupSettings asset;
    [Space]
    private Stack<BasePopup> popupStack = new();
    private Dictionary<Type, BasePopup> popups = new();
    private BackgroundPopupHandler bgHandler;

    public BasePopup Current => popupStack.Count == 0 ? null : popupStack.Peek();

#if UNITY_EDITOR
    [Space]
    public List<BasePopup> debugPopup = new();
    private void Update() {
        debugPopup = popupStack.ToList();
    }
#endif

    private void Awake() {
        bgHandler = GetComponent<BackgroundPopupHandler>();
        DontDestroyOnLoad(gameObject);

        foreach (var popup in GetComponentsInChildren<BasePopup>(true)) {
            //var prefab = Instantiate(popup, transform);
            popup.gameObject.SetActive(true);
            popup.root.gameObject.SetActive(false);
            popups[popup.GetType()] = popup;
        }
    }

    public T Get<T>() where T : BasePopup {
        return popups[typeof(T)] as T;
    }

    public async UniTask<T> Render<T>(object ctx = null) where T : BasePopup {
        T popup = Get<T>();

        if (popupStack.Count > 0) {
            await popupStack.Peek().Close();
        }

        popupStack.Push(popup);

        bgHandler.Render();
        await popup.Render(ctx);

        //BACKGROUND
        //background.SetAlpha(0.7f);
        //background.SetAlpha(0);
        //background.gameObject.SetActive(true);
        //background.DOFade(0.7f, fadeDuration).SetUpdate(true);
        //

        return popup;
    }

    public async UniTask Close() {
        if (popupStack.Count == 0) {
            return;
        }

        var currentPopup = popupStack.Pop();

        if (popupStack.Count == 0) {
            bgHandler.Close();
        }

        await currentPopup.Close();

        if (popupStack.Count > 0) {
            await popupStack.Peek().Render();
        }
    }

    //public T Close<T>() where T : BasePopup {
    //    T popup = Get<T>();

    //    popup.Close();

    //    //BACKGROUND
    //    background.DOFade(0, asset.FadeOffDuration).SetUpdate(true).OnComplete(() => {
    //        background.gameObject.SetActive(false);
    //    });
    //    //
    //    return popup;
    //}

    //public async UniTask<T> CloseAsync<T>() where T : BasePopup {
    //    T popup = Get<T>();
    //    popup.Close();
    //    await background.DOFade(0, asset.FadeOffDuration).SetUpdate(true);
    //    background.gameObject.SetActive(false);
    //    return popup;
    //}

    //public T Close<T>(T type) where T : BasePopup {
    //    return Close<T>();
    //}

    public void CloseAll() {
        //foreach (var popup in popups) {
        //    popups[popup.Key].Close();
        //}
        bgHandler.CloseForce();
        //current = null;

        while (popupStack.Count > 0) {
            popupStack.Pop().Close();
        }
    }
}
