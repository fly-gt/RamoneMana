using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : ScreenBase {
    public Image wrongImage;
    public TMP_Text scoreText;
    public ScoreView scoreView;
    public TextFlying flying;

    public void PauseClick() {
        if (FlyShared.Instance.StarFly.IsFlying) {
            return;
        }

        GameController.Instance.ToPause();
    }

    public async UniTask WrongEffect() {
        if (wrongImage) {
            await wrongImage.DOFade(0.3f, 0.2f);
            await wrongImage.DOFade(0f, 0.2f);
        }
    }

    public void ScoreTextFly(int value, bool add) {
        var randomX = new Vector3(Random.Range(-0.5f, 0.8f), 0, 0);
        var randomY = Vector3.up * Random.Range(0.7f, 1f);
        var endPos = scoreView.transform.position - randomY + randomX;
        var flyText = (add ? "+" : "-") + value.ToString();
        var color = add ? Color.green : Color.red;
        Debug.Log(flyText);
        flying.Fly(flyText, color, scoreView.transform.position, endPos);
    }
}
