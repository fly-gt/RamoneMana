using UnityEngine.UI;

public class LosePopup : BasePopup {
    public Button toMainButton;

    //private void Awake() {
    //    toMainButton.onClick.AddListener(OnToMainClick);
    //}

    //private void OnDestroy() {
    //    toMainButton.onClick.RemoveAllListeners();
    //}

    public void Restart() {
        AppShared.Instance.appState.SetState<GameState>();
        PopupManager.Instance.Close();
    }
}
