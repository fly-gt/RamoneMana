using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SceneTransfer : MonoBehaviour {
    [SerializeField] private CanvasGroup canvasGroup;
    private CanvasGroupFade canvasGroupFade;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        canvasGroupFade = new CanvasGroupFade(canvasGroup);
        canvasGroup.alpha = 0f;
    }

    public async UniTask TransferTo(SceneName sceneName) {
        await canvasGroupFade.SetWithFade(true);
        var oldSceneEntry = GameObject.FindAnyObjectByType<SceneEntryBase>();
        oldSceneEntry?.Clear();
        await SceneLoader.Load(sceneName);
        var newSceneEntry = GameObject.FindAnyObjectByType<SceneEntryBase>();

        if (newSceneEntry) {
            await newSceneEntry.Initialize();
        }

        await UniTask.Delay(500);
        await canvasGroupFade.SetWithFade(false);
    }

    public static async UniTask Transfer2(SceneName sceneName) {
        var go = await Addressables.InstantiateAsync("SceneTransfer");
        SceneTransfer transfer = go.GetComponent<SceneTransfer>();
        await transfer.TransferTo(sceneName);
        Addressables.ReleaseInstance(go);
    }
}
