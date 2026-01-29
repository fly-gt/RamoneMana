using UnityEngine;

[RequireComponent (typeof(ObjectPool))]
public class AudioService : MonoBehaviour, IAudioService {
    public Audio audioPrefab;
    public ObjectPool pool;

    public async void Play(IAudioAsset asset, AudioPlayData data) {
        if (asset == null) return;
        if (asset is not AudioEventAsset ae) return;

        int randomClip = Random.Range(0, ae.clip.Count);
        float randomPitch = Random.Range(ae.pitch.x, ae.pitch.y);

        GameObject go = await pool.Get();
        Audio audio = go.GetComponent<Audio>();
        audio.Initialize(pool);

        //set data
        go.transform.position = data.Position;
        //

        //asset settings
        audio.audioSource.clip = ae.clip[randomClip];
        audio.audioSource.volume = ae.volume;
        audio.audioSource.pitch = randomPitch;
        //

        audio.Play();
    }
}
