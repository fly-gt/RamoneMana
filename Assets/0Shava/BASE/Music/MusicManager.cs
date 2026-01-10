using Cysharp.Threading.Tasks;
using DG.Tweening;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;

public class MusicManager : Singletone<MusicManager> {
    public AudioClip[] clips;
    [Space]
    public AudioClip current;
    public AudioSource audioSource;
    public int iterator;
    public bool started;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    //igraem po krygy 
    private void Update() {
        if (!started || audioSource.time < audioSource.clip.length) {
            return;
        }

        ResetMusic();
    }

    public void ResetMusic() {
        if (clips.Length <= 0) {
            return;
        }

        current = clips[iterator++ % clips.Length];

        if (audioSource == null) {
            audioSource = transform.AddComponent<AudioSource>();
        }

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
            await audioSource.DOFade(0, fade).ToUniTask();
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
