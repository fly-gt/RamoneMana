using Cysharp.Threading.Tasks;
using System.Collections.Generic;

public class SimpleInit : IInitializable {
    private List<UniTask> list = new();
    public async UniTask InitializeAsync() {
        list = new() {
            UtilityAdressables.InitializeObject<AppShared>(),
            UtilityAdressables.InitializeObject<PopupManager>(),
            UtilityAdressables.InitializeObject<MusicManager>(),
            UtilityAdressables.InitializeObject<ServiceLocator>(),
        };

        await UniTask.WhenAll(list);
        list.Clear();
    }
}

public interface IInitializable {
    UniTask InitializeAsync();
}
