using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public class ScoreFlying : Singletone<ScoreFlying> {
    public ObjectPool pool;
    public float duration = 0.6f;
    public float startScale = 1f;
    public float endScale = 0.2f;
    public Ease moveEase = Ease.InOutQuad;

    public async void Fly(Vector3 startPos, Vector3 endPos) {
        //var gameObj = Instantiate(prefab, startPos, prefab.transform.rotation);
        var gameObj = await pool.Get();
        gameObj.transform.position = startPos;
        gameObj.transform.localScale = Vector3.one * startScale;

        gameObj.transform.DOMove(endPos, duration).SetEase(moveEase);
        gameObj.transform.DOScale(endScale, duration).SetEase(Ease.OutQuad).OnComplete(() => {
            //Destroy(gameObj); 
            pool.Return(gameObj);
        });
    }
}
