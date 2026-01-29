using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader {
    public static async UniTask Load(SceneName sceneName) {
        Scene oldScene = SceneManager.GetActiveScene();
        string newSceneName = Enum.GetName(typeof(SceneName), sceneName);
        await SceneManager.LoadSceneAsync(newSceneName, LoadSceneMode.Additive);
        Scene scene = SceneManager.GetSceneByName(newSceneName);
        SceneManager.SetActiveScene(scene);
        await SceneManager.UnloadSceneAsync(oldScene.name);
    }
}

public enum SceneName {
    Root,
    Main,
    Game
}