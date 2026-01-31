using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

public class AddressablesInit : IInitializable {
    public async UniTask InitializeAsync() {
        await Addressables.InitializeAsync().Task;
        UtilityAdressables.InitializeCache();
    }
}
