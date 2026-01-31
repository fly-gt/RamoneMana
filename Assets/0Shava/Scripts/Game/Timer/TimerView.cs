using TMPro;
using UnityEngine;

public class TimerView : MonoBehaviour {
    public TMP_Text timerView;

    public void SetTimeMMSS(float elapsedSeconds) {
        int totalSeconds = (int)elapsedSeconds;
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        // аег бшдекемхъ оюлърх
        timerView.SetText("{0:00}:{1:00}", minutes, seconds);
    }
}
