using System;

public class AdService {
    private IAdProvider adProvider;

    public event Action OnOpenInterAdv;
    public event Action OnCloseInterAdv;
    public event Action OnOpenAnyAdv;
    public event Action OnCloseAnyAdv;

    public AdService(IAdProvider adProvider) {
        this.adProvider = adProvider;
        this.adProvider.Initialize();

        adProvider.OnOpenInterAdv += OpenInterAdv;
        adProvider.OnCloseInterAdv += CloseInterAdv;
        adProvider.OnOpenAnyAdv += OpenAnyAdv;
        adProvider.OnCloseAnyAdv += CloseAnyAdv;
    }

    public void ShowInterstitial() {
        if (adProvider.IsReady()) {
            adProvider.ShowInterstitial();
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

public interface IAdProvider {
    event Action OnOpenInterAdv;
    event Action OnCloseInterAdv;
    event Action OnOpenAnyAdv;
    event Action OnCloseAnyAdv;

    void Initialize();
    bool IsReady();
    void ShowInterstitial();
}
