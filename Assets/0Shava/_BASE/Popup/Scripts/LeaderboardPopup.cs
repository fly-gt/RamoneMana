using Cysharp.Threading.Tasks;

public class LeaderboardPopup : BasePopup {
    public void BackClick() {
        //PopupManager.Instance.Close<LeaderboardPopup>();
        PopupManager.Instance.Close();
    }
}
