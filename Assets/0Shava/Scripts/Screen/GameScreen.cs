using UnityEngine;

public class GameScreen : ScreenBase {
    public void PauseClick() {
        PopupManager.Instance.Render<PausePopup>();
    }
}
