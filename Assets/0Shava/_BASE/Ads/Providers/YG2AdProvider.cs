using System;
using YG;

public class YG2AdProvider : IAdProvider {
    public event Action OnOpenInterAdv;
    public event Action OnCloseInterAdv;
    public event Action OnOpenAnyAdv;
    public event Action OnCloseAnyAdv;

    public void Initialize() {
        YG2.onOpenInterAdv += OpenInterAdv;
        YG2.onCloseInterAdv += CloseInterAdv;
        YG2.onOpenAnyAdv += OpenAnyAdv;
        YG2.onCloseAnyAdv += CloseAnyAdv;
    }

    public bool IsReady() {
        return YG2.isSDKEnabled && YG2.isTimerAdvCompleted;
    }

    public void ShowInterstitial() {
        YG2.InterstitialAdvShow();
    }

    private void OpenInterAdv() {
        OnOpenInterAdv?.Invoke();
    }

    private void CloseInterAdv() {
        OnCloseInterAdv?.Invoke();
    }

    private void OpenAnyAdv() {
        OnOpenAnyAdv?.Invoke();
    }

    private void CloseAnyAdv() {
        OnCloseAnyAdv?.Invoke();
    }
}
