using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameEntry : SceneEntryBase {
    public GameController gameController;

    public override async UniTask Initialize() {
        await base.Initialize();
        gameController.ToMenu();
        //gameController.Initialize();
    }

    public override void Clear() {
        base.Clear();
        //gameController.Clear();
    }
}
