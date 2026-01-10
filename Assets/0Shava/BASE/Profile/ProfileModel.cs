using System;

[Serializable]
public class ProfileModel {
    public int BestScore;
    public int ScoreGeneral;
    public int Level;
    public bool HasTutorial = true;
    public bool GameEnd;

    public event Action<int> ChangeScoreGeneral;

    public void SetScoreGeneral(int score) {
        ScoreGeneral = score;
        PlayerPrefsStorage<ProfileModel>.Save("profile", this);
        ChangeScoreGeneral?.Invoke(score);
    }

    public void SetBestScore(int bestScore) {
        BestScore = bestScore;
        PlayerPrefsStorage<ProfileModel>.Save("profile", this);
    }

    public void SetTutorial(bool value) {
        HasTutorial = value;
        PlayerPrefsStorage<ProfileModel>.Save("profile", this);
    }

    public void SetLevel(int level) {
        Level = level;
        PlayerPrefsStorage<ProfileModel>.Save("profile", this);
    }

    public void SetGameEnd() {
        GameEnd = true;
    }
}
