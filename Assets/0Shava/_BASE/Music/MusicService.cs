using System.Collections.Generic;
using UnityEngine;

public class MusicService : MonoBehaviour, IMusicService {
    public List<AudioClip> musicTracks; // Список музыки

    private AudioSource audioSource;
    private int currentTrackIndex = 0;
    private bool isPlaying = false;     // Флаг, играет ли музыка

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false; // Мы сами будем переключать треки
        DontDestroyOnLoad(gameObject);
    }

    public void StartMusic() {
        if (musicTracks == null || musicTracks.Count == 0) {
            Debug.LogWarning("MusicService: Список треков пуст!");
            return;
        }

        isPlaying = true;
        currentTrackIndex = 0;
        PlayTrack(currentTrackIndex);
    }

    public void PauseMusic() {
        if (isPlaying && audioSource.isPlaying)
            audioSource.Pause();
    }

    public void ResumeMusic() {
        if (isPlaying && audioSource.clip != null && !audioSource.isPlaying)
            audioSource.UnPause();
    }

    public void NextTrack() {
        if (musicTracks == null || musicTracks.Count == 0) return;

        currentTrackIndex = (currentTrackIndex + 1) % musicTracks.Count;
        PlayTrack(currentTrackIndex);
    }

    private void PlayTrack(int index) {
        if (musicTracks == null || musicTracks.Count == 0) return;

        audioSource.clip = musicTracks[index];
        audioSource.Stop();
        audioSource.time = 0;
        audioSource.Play();
    }

    private void Update() {
        // Если музыка играет и трек закончился, запускаем следующий
        if (isPlaying && !audioSource.isPlaying && musicTracks.Count > 0) {
            currentTrackIndex = (currentTrackIndex + 1) % musicTracks.Count;
            PlayTrack(currentTrackIndex);
        }
    }

    private void OnApplicationPause(bool pause) {
        if (isPlaying) {
            if (pause)
                audioSource.Pause();
            else
                audioSource.UnPause();
        }
    }

    private void OnApplicationFocus(bool hasFocus) {
        if (isPlaying) {
            if (!hasFocus)
                audioSource.Pause();
            else
                audioSource.UnPause();
        }
    }
}

public interface IMusicService {
    void StartMusic();
}