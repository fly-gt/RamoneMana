using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePopup : BasePopup {
    public Button againBtn;
    public TMP_Text scoreText;
    public bool congratulationsOneTime = false;
    public GameObject[] goOnceCongratulations;

    private EndGamePopupCtx endGamePopupCtx;

    public override async UniTask Render(object ctx = null) {
        if (ctx is EndGamePopupCtx endGamePopupCtx) {
            //scoreText.text = $"{endGamePopupCtx}";
            scoreText.SetText("{0}", endGamePopupCtx.score);
        }

        if (!congratulationsOneTime) {
            //first Time
            congratulationsOneTime = true;
        } else {
            //second time
            foreach (var g in goOnceCongratulations) {
                g.gameObject.SetActive(false);
            }
        }
        await base.Render(ctx);
    }

    public void AgainClick() {
        //MusicUtility.StartMusic();
        //PlayerController.Instance.model.CleanWitchTutorial();
        ProfileManager.Instance.Clear();
        AppShared.Instance.appState.SetState<GameState>();
        PopupManager.Instance.Close();
        //YG2.InterstitialAdvShow();

        //PopupManager.Instance.Close(this);
        //AppShared.Instance.AppMachine.SetState<GameState>();
    }

    public void LeaderboardClick() {
        //MusicUtility.StartMusic();
        PopupManager.Instance.Render<LeaderboardPopup>();
    }
}

public struct EndGamePopupCtx {
    public int score;
}
