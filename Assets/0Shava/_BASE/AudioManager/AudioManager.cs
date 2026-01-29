using UnityEngine;

public class AudioManager : Singletone<AudioManager> {
    public IAudioService service;

    private void Awake() {
        service = GetComponent<IAudioService>();
        DontDestroyOnLoad(gameObject);
    }

    public static void TryPlay(IAudioAsset ae, AudioPlayData data) {
        if (!Instance) {
            return;
        }

        Instance.service.Play(ae, data);
    }
}

public interface IAudioService {
    void Play(IAudioAsset asset, AudioPlayData data);
}

public interface IAudioAsset {
}

//can be rework
public struct AudioPlayData {
    public Vector3 Position;
}