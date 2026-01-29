using System;

public class ScoreModel {
    public int Score;
    public event Action<int> OnChangeScore; 

    public void SetScore(int score) {
        Score = score;
        OnChangeScore?.Invoke(Score);
    }
}
