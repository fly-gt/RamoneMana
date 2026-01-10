using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

public class SceneEntryBase : MonoBehaviour {
    public virtual async UniTask Initialize() {
        await ScreenManager.Instance.Initialize();
    }

    public virtual void Clear() { }
}
