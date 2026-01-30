using UnityEngine;

public class ScoreController : MonoBehaviour {
    public ScoreModel model;
    public ScoreView view;

    public Vector3 ViewWorldPostion => view.transform.position;

    public int Score => model.Score;

    private void Awake() {
        model = new();
        model.OnChangeScore += ChangeScore;
    }

    private void OnDisable() {
        model.OnChangeScore -= ChangeScore;
    }

    public void Setup() {
        view = ScreenManager.Instance.Get<GameScreen>().scoreView;
        model.SetScore(0);
    }

    public void AddScore(int score, bool pulse = false) {
        model.SetScore(model.Score + score);

        if (pulse) {
            view.AddScoreVFX();
        }
        //flying.Fly(pos + Vector3.up, view.ScoreFlyRect.transform.position);
    }

    public void SubtractScore(int score) {
        var clampScore = Mathf.Clamp(model.Score - score, 0, int.MaxValue);
        model.SetScore(clampScore);
    }

    private void ChangeScore(int score) {
        view.SetScore(score);
    }
}
