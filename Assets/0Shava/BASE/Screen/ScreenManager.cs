using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class ScreenManager : Singletone<ScreenManager> {
    [SerializeField] private List<AssetReference> screenAssets;
    [SerializeField] private GraphicRaycaster raycaster;
    public Canvas canvas;
    public RectTransform panel;

    private Dictionary<Type, ScreenBase> screens = new();

    public ScreenBase Current { get; private set; }
    public bool InProcess { get; private set; }

    public async UniTask Initialize() {
        //screens = FindObjectsOfType<ScreenBase>(true).ToDictionary(x => x.GetType());

        foreach (var screen in screenAssets) {
            var parent = panel != null ? panel : transform;
            var s =  await UtilityAdressables.InitializeObject<ScreenBase>(screen, parent);
            s.gameObject.name = s.GetType().Name;
            screens.Add(s.GetType(), s);
            //Debug.Log($"{s.GetType()} {s}");
        }

        foreach (var key in screens.Keys) {
            screens[key].Initialize();
            screens[key].gameObject.SetActive(false);
        }
    }

    #region SET
    public async UniTask Set<Y>(object ctx = null, bool fade = true) where Y : ScreenBase {
        await Set(typeof(Y), ctx, fade);
    }

    public async UniTask Set(Type type, object ctx = null, bool fade = true) {
        if (!screens.ContainsKey(type)) {
            Debug.LogError($"Cant find {type.Name}");
            return;
        }

        if (InProcess) {
            return;
        }

        InProcess = true;

        await DoSet(type, fade, ctx);
    }

    private async UniTask DoSet(Type type, bool fade, object ctx = null) {
        raycaster.enabled = false;

        if (Current) {
            await Current.Close(fade);
        }

        Current = screens[type];
        await Current.Render(ctx, fade);
        raycaster.enabled = true;
        InProcess = false;
    }
    #endregion

    #region CLOSE
    public async UniTask Close() {
        if (InProcess) {
            return;
        }

        InProcess = true;
        await DoClose();
    }

    private async UniTask DoClose() {
        raycaster.enabled = false;

        if (Current) {
            await Current.Close(true);
            Current = null;
        }

        raycaster.enabled = true;
        InProcess = false;
    }

    #endregion

    public Y Get<Y>() where Y : ScreenBase {
        return screens[typeof(Y)] as Y;
    }
}
