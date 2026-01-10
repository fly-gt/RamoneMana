using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializeApp {
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void BeforeSceneLoad() {
        Application.targetFrameRate = 60;

        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.name != "Root") {
            SceneLoader.Load(SceneName.Root);
        }

        //InitializeObject<FPSCounter>();
        //InitializeObject<PlayerController>();
        //addressable
        //InitializeObject<AppShared>();
        ////InitializeObject<CursorBehaviour>();
        //InitializeObject<CoroutineManager>();
        //InitializeObject<PopupManager>();
        ////InitializeObject<AssetsShared>();
        //InitializeObject<Localization>();
        //InitializeObject<TutorialController>();
        //InitializeObject<AudioManager>();
        //InitializeObject<MusicManager>();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void AfterSceneLoad() {
       
    }
}