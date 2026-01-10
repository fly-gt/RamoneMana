using UnityEngine;

public class MenuScreen : ScreenBase {
    public void StartClick() {
        ScreenManager.Instance.Set<GameScreen>();
    }
}
