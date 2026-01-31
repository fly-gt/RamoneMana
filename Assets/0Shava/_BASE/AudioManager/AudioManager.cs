using UnityEngine;

public class AudioManager : MonoBehaviour {
    private IAudioService service;

    private void Awake() {
        service = GetComponent<IAudioService>();
        DontDestroyOnLoad(gameObject);
    }

    public void TryPlay(IAudioAsset ae, AudioPlayData data) {
        service.Play(ae, data);
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