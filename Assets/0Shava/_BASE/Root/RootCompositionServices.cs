using Cysharp.Threading.Tasks;
using UnityEngine;

public class RootCompositionServices : MonoBehaviour {
    public async UniTask Initialize() {
        AdService adService = new AdService(AdProviderFactory.Create());
        IAudioService audioManager = await UtilityAdressables.InitializeObject<IAudioService>("AudioService");
        IMusicService musicManager = await UtilityAdressables.InitializeObject<IMusicService>("MusicService");

        ServiceLocator.Register(adService);
        ServiceLocator.Register(audioManager);
        ServiceLocator.Register(musicManager);
    }
}
