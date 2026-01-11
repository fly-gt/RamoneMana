using UnityEngine;

public class MenuScreen : ScreenBase {
    public void StartClick() {
        GameController.Instance.ToGameplay();
        //ScreenManager.Instance.Set<GameScreen>();
    }

    public void LeaderboardClick() {

    }

    public void SettingsClick() {

    }
}
