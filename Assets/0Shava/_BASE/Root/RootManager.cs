using Cysharp.Threading.Tasks;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class RootManager : MonoBehaviour {
    public RootLoader loader;
    private List<IInitializable> inited = new();

    private void Awake() {
        inited.Add(new AddressablesInit());
        inited.Add(new SimpleInit());
    }

    private async void Start() {
        await Addressables.InitializeAsync().Task;
        loader.SetProgress(0, inited.Count);
        await UniTask.Delay(1000);

        for (int i = 0; i < inited.Count; i++) {
            await inited[i].InitializeAsync();
            loader.SetProgress((i + 1), inited.Count);
            await UniTask.Delay(1000);
        }

        inited.Clear();
        SceneTransfer.Transfer2(SceneName.Game);
    }
}

public interface IInitializable {
    UniTask InitializeAsync();
}

public class SimpleInit : IInitializable {
    private List<UniTask> list = new();
    public async UniTask InitializeAsync() {
        list = new() {
            UtilityAdressables.InitializeObject<AppShared>(),
            UtilityAdressables.InitializeObject<PopupManager>(),
            UtilityAdressables.InitializeObject<AudioManager>(),
            UtilityAdressables.InitializeObject<MusicManager>(),
        };

        await UniTask.WhenAll(list);

    }
}

public class AddressablesInit : IInitializable {
    public async UniTask InitializeAsync() {
        await Addressables.InitializeAsync().Task;
        UtilityAdressables.InitializeCache();
    }
}
