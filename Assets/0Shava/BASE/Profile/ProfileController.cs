using Newtonsoft.Json;
using System;
using UnityEngine;

public class ProfileController : Singletone<ProfileController> {
    private ProfileModel model;

    public event Action<int> OnChangePoints;

    public int ScoreGeneral => model.ScoreGeneral;
    public bool HasTutorial => model.HasTutorial;
    public int Level => model.Level;
    public bool GameEnded => model.GameEnd;
    public int BestScore => model.BestScore;

    private void Awake() {
        model = PlayerPrefsStorage<ProfileModel>.Load("profile");

        if (model.HasTutorial) {
            model = new();
            PlayerPrefsStorage<ProfileModel>.Save("profile", model);
        }

        Debug.Log(JsonConvert.SerializeObject(model));
    }

    public void SetScoreGeneral(int score) {
        model.SetScoreGeneral(score);
    }

    public void SetLevel(int level) {
        model.SetLevel(level);
    }

    public void SetTutorial(bool value) {
        model.SetTutorial(value);
    }

    public void EndGame() {
        model.SetGameEnd();
    }

    public void Clear() {
        var oldBestScore = model.BestScore;
        var oldHasTutor = model.HasTutorial;

        model = new();
        model.BestScore = oldBestScore;
        model.HasTutorial = oldHasTutor;

        PlayerPrefsStorage<ProfileModel>.Save("profile", model);
    }
}
