using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour {
    public TMP_Text scoreText;
    private readonly string ex = "{0}";
    private Sequence addScoreTween;

    public void SetScore(int score) {
        if (scoreText) {
            scoreText.SetText(ex, score);
        }
    }

    public void AddScoreVFX() {
        addScoreTween?.Kill();
        addScoreTween = DOTween.Sequence();
        addScoreTween.Append(transform.DOScale(0.6f, 0.05f));
        addScoreTween.Append(transform.DOScale(1, 0.05f));
    }
}