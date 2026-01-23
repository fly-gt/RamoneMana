using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class GameScreen : ScreenBase {
    public Image wrongImage;
    public TMP_Text scoreText;
    public ScoreView scoreView;

    public void PauseClick() {
        GameController.Instance.ToPause();
    }

    public async UniTask WrongEffect() {
        if (wrongImage) {
            await wrongImage.DOFade(0.3f, 0.2f).ToUniTask();
            await wrongImage.DOFade(0f, 0.2f).ToUniTask();
        }
    }


}
