using Cysharp.Threading.Tasks;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : Singletone<MusicManager> {
    public AudioClip[] clips;
    [Space]
    public AudioClip current;
    public AudioSource audioSource;
    public int iterator;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    //igraem po krygy 
    private void Update() {
        if (audioSource.clip == null || audioSource.time < audioSource.clip.length) {
            return;
        }

        ResetMusic();
    }

    public void ResetMusic() {
        if (clips.Length <= 0) {
            return;
        }

        current = clips[iterator++ % clips.Length];

        audioSource.clip = current;
        audioSource.volume = 1;
        audioSource.Play();
    }

    public void Pause() {
        audioSource.Pause();
    }

    public void Play() {
        audioSource.Play();
    }

    public async void Stop(float fade = 0) {
        if (!Mathf.Approximately(fade, 0)) {
            await audioSource.DOFade(0, fade);
        }

        audioSource.Stop();
    }

    [Button("TEST")]
    public void Tedst() {
        for (int i = 0; i < 10; i++) {
            Debug.Log(i % 3);
        }
    }
}
