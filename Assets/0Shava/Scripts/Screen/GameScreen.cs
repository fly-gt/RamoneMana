using UnityEngine;

public class GameScreen : ScreenBase {
    public void PauseClick() {
        GameController.Instance.ToPause();
    }
}
