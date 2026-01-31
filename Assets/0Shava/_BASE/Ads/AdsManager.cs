public class AdsManager : Singletone<AdsManager> {
    public AdService AdService { get; private set; }

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        IAdProvider provider = AdProviderFactory.Create();
        AdService = new AdService(provider);
    }
}
