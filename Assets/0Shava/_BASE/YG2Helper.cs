#if PLUGIN_YG_2
using UnityEngine;
using YG;

public class YG2Helper : Singletone<YG2Helper> {
    private bool inited;
    void Awake() {
        YG2.onOpenAnyAdv += OnOpenAnyAdv;
        YG2.onCloseAnyAdv += OnCloseAnyAdv;
        YG2.onFocusWindowGame += onFocusWindowGame;
        YG2.onPauseGame += onPauseGame;
        YG2.onShowWindowGame += onShowWindowGame;
        YG2.onHideWindowGame += onHideWindowGame;
    }

    void OnDestroy() {
        YG2.onOpenAnyAdv -= OnOpenAnyAdv;
        YG2.onCloseAnyAdv -= OnCloseAnyAdv;
        YG2.onFocusWindowGame -= onFocusWindowGame;
        YG2.onPauseGame -= onPauseGame;
        YG2.onShowWindowGame -= onShowWindowGame;
        YG2.onHideWindowGame -= onHideWindowGame;
    }

    void OnOpenAnyAdv() {
        YG2.PauseGame(true);
        Time.timeScale = 0;
    }

    void OnCloseAnyAdv() {
        YG2.PauseGame(false);
        Time.timeScale = 1;
    }

    void onFocusWindowGame(bool focus) {
        Debug.Log($"onFocusWindowGame {focus}");
    }

    void onPauseGame(bool pause) {
        YG2.PauseGame(pause);
        Time.timeScale = pause ? 0 : 1;
        Debug.Log($"onPauseGame {pause}");
    }

    void onShowWindowGame() {
        Debug.Log($"onShowWindowGame");
    }

    void onHideWindowGame() {
        Debug.Log($"onHideWindowGame");
    }

    public bool TryInit() {
        if (inited) {
            return false;
        }

        inited = true;
        YG2.GameplayStart();
        return true;
    }
}
#endif