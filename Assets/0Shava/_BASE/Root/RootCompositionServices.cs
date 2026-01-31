using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class RootCompositionServices : MonoBehaviour {
    public new AssetReference audio;
    public AssetReference music;

    public async UniTask Initialize() {
        AdService adService = new AdService(AdProviderFactory.Create());
        IAudioService audioManager = await UtilityAdressables.InitializeObject<IAudioService>(audio);
        IMusicService musicManager = await UtilityAdressables.InitializeObject<IMusicService>(music);

        ServiceLocator.Register(adService);
        ServiceLocator.Register(audioManager);
        ServiceLocator.Register(musicManager);
    }
}
