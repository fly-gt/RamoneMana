using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class RootManager : MonoBehaviour {
    public RootLoader loader;
    public RootCompositionServices rootCompositionServices;
    private List<IInitializable> inited = new();

    private void Awake() {
        inited.Add(new AddressablesInit());
        inited.Add(new SimpleInit());
    }

    private void Start() {
        Start_Async();
    }

    private async void Start_Async() {
        var count = inited.Count + 1;

        loader.SetProgress(0, count);
        await UniTask.Delay(100);

        for (int i = 0; i < inited.Count; i++) {
            await inited[i].InitializeAsync();
            loader.SetProgress((i + 1), count);
            await UniTask.Delay(100);
        }

        await rootCompositionServices.Initialize();
        loader.SetProgress(count, count);

        inited.Clear();
        SceneTransfer.Transfer2(SceneName.Game);
    }
}

