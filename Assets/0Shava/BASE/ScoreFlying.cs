using DG.Tweening;
using UnityEngine;

public class ScoreFlying : MonoBehaviour {
    public GameObject prefab;
    public float duration = 0.6f;
    public float startScale = 1f;
    public float endScale = 0.2f;
    public Ease moveEase = Ease.InOutQuad;

    public void Fly(Vector3 startPos, Vector3 endPos) {
        var gameObj = Instantiate(prefab, startPos, prefab.transform.rotation);
        gameObj.transform.localScale = Vector3.one * startScale;

        gameObj.transform.DOMove(endPos, duration).SetEase(moveEase);
        gameObj.transform.DOScale(endScale, duration).SetEase(Ease.OutQuad).OnComplete(() => {
            Destroy(gameObj); // можно отключить или заменить
        });
    }
}
