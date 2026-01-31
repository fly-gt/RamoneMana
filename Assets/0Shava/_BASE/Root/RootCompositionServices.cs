using Cysharp.Threading.Tasks;

public static class RootCompositionServices  {
    public static async UniTask Initialize() {
        AdService adService = new AdService(AdProviderFactory.Create());
        AudioManager audioManager = await UtilityAdressables.InitializeObject<AudioManager>();

        ServiceLocator.Register(adService);
        ServiceLocator.Register(audioManager);
    }
}
