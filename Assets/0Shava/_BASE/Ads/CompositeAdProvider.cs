using System;
using System.Collections.Generic;

public class CompositeAdProvider : IAdProvider {
    private List<IAdProvider> providers;
    public event Action OnOpenInterAdv;
    public event Action OnCloseInterAdv;
    public event Action OnOpenAnyAdv;
    public event Action OnCloseAnyAdv;

    public CompositeAdProvider(params IAdProvider[] providers) {
        this.providers = new List<IAdProvider>();

        for (int i = 0; i < providers.Length; i++) {
            this.providers.Add(providers[i]);
        }
    }

    public void Initialize() {
        for (int i = 0; i < providers.Count; i++) {
            providers[i].Initialize();
            providers[i].OnOpenInterAdv += OpenInterAdv;
            providers[i].OnCloseInterAdv += CloseInterAdv;
            providers[i].OnOpenAnyAdv += OpenAnyAdv;
            providers[i].OnCloseAnyAdv += CloseAnyAdv;
        }
    }

    public bool IsReady() {
        for (int i = 0; i < providers.Count; i++) {
            if (providers[i].IsReady())
                return true;
        }

        return false;
    }

    public void ShowInterstitial() {
        for (int i = 0; i < providers.Count; i++) {
            if (providers[i].IsReady()) {
                providers[i].ShowInterstitial();
                return;
            }
        }
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
