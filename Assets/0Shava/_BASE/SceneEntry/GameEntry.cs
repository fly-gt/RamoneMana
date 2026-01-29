using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;

public class GameEntry : SceneEntryBase {
    public GameController gameController;

    public override async UniTask Initialize() {
        await base.Initialize();
        await gameController.Initialize();
        //gameController.Initialize();
    }

    public override void Clear() {
        base.Clear();
        //gameController.Clear();
    }

    [Button]
    private void Test() {
        for (int i = 0; i < 25; i++) {
            Debug.Log($"{i}: {(i) % 5 == 0}");
        }
    }
}
