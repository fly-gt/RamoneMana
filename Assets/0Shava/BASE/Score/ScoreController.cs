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

    public void AddScore(int value) {
        model.SetScore(model.Score + value);
        //flying.Fly(pos + Vector3.up, view.ScoreFlyRect.transform.position);
    }

    public void SetActiveView(bool value) {
        view.SetActive(value);
    }

    private void ChangeScore(int score) {
        view.SetScore(score);
    }
}
