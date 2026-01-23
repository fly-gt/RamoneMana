using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour {
    public TMP_Text scoreText;
    private readonly string ex = "{0}";

    public void SetScore(int score) {
        if (scoreText) {
            scoreText.SetText(ex, score);
        }
    }

    public void SetActive(bool value) {
        //ScreenManager.Instance.Get<GameScreen>().scoreRect.gameObject.SetActive(value);
    }
}