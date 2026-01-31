using System;
using UnityEngine;

public class AppEventsService {
    public event Action OnPaused;
    public event Action OnResume;

    public void Pause() {
        OnPaused?.Invoke();
        Time.timeScale = 0f;
    }

    public void Resume() {
        Time.timeScale = 1f;
        OnResume?.Invoke();
    }
}
