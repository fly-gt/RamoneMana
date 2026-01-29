using System.Collections;
using UnityEngine;

public class CoroutineManager : MonoBehaviour {
    private static CoroutineManager instance;

    public static void Initialize() {
        var newObj = new GameObject(nameof(CoroutineManager));
        newObj.AddComponent<CoroutineManager>();
    }

    private void Awake() {
        if (instance == null) {
            instance = FindAnyObjectByType<CoroutineManager>();
            DontDestroyOnLoad(instance.gameObject);
        }
    }

    public static Coroutine Run(IEnumerator coroutine) {
        return instance.StartCoroutine(coroutine);
    }

    public static void TryStop(ref Coroutine coroutine) {
        if (instance == null) {
            return;
        }

        if (coroutine != null) {
            instance.StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    private void OnDisable() {
        if (instance) {
            instance.StopAllCoroutines();
        }
    }
}
