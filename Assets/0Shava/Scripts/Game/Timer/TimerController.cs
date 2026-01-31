using UnityEngine;

public class TimerController : MonoBehaviour {
    public TimerView view;

    private float elapsedSeconds;
    private bool isRunning;

    void LateUpdate() {
        if (!isRunning)
            return;

        elapsedSeconds += Time.deltaTime;
        view.SetTimeMMSS(elapsedSeconds);
    }

    public void Setup() {
        view = ScreenManager.Instance.Get<GameScreen>().timerView;
    }

    public void StartTimer() {
        isRunning = true;
    }

    public void StopTimer() {
        isRunning = false;
    }

    public void ResetTimer() {
        elapsedSeconds = 0f;
        view.SetTimeMMSS(elapsedSeconds);
    }
}
