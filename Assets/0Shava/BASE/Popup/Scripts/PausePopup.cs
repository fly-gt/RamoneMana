using Cysharp.Threading.Tasks;

public class PausePopup : BasePopup {
    public void MenuClick() {
        PopupManager.Instance.CloseAll();
        //ScreenManager.Instance.Set<MenuScreen>();
        GameController.Instance.ToMenu();
    }

    public void ContinueClick() {
        PopupManager.Instance.CloseAll();
    }
}
