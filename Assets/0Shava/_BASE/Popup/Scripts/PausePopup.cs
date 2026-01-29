using Cysharp.Threading.Tasks;

public class PausePopup : BasePopup {
    public void MenuClick() {
        PopupManager.Instance.CloseAll();
        GameController.Instance.ToMenu();
    }

    public void ContinueClick() {
        GameController.Instance.State = GameStateType.Game;
        PopupManager.Instance.CloseAll();
    }
}
