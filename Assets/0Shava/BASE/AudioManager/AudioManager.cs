using UnityEngine;

public class AudioManager : Singletone<AudioManager> {
    public Audio audioPrefab;
    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    public void TryPlay(AudioEventAsset ae, Vector3 pos) {
        if (!ae) {
            return;
        }

        //if (!AppShared.Instance.AppInited) {
        //    return;
        //}

        var randomClip = Random.Range(0, ae.clip.Count);
        var randomPitch = Random.Range(ae.pitch.x, ae.pitch.y);
        Audio go = Instantiate(audioPrefab);
        go.transform.position = pos;
        go.audioSource.clip = ae.clip[randomClip];
        go.audioSource.volume = ae.volume;
        go.audioSource.pitch = randomPitch;
        go.Play();
    }

    public AudioEventAsset test;

    private void Update() {
        //if (Input.GetKeyDown(KeyCode.Space)) {
        //    TryPlay(test, PlayerView.Instance.transform.position);
        //}
    }
}
