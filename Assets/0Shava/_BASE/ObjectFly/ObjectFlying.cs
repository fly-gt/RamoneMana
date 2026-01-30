using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public class ObjectFlying : MonoBehaviour {
    public ObjectPool pool;
    [Space]
    public float duration = 0.6f;
    public Ease moveEase = Ease.InOutQuad;
    [Space]
    public float startScale = 1f;
    public float endScale = 1f;

    public bool IsFlying;

    public async void Fly(Vector3 startPos, Vector3 endPos, Action completed = null) {
        IsFlying = true;
        GameObject gameObj = await pool.Get();

        FlyBehaviour(gameObj);

        gameObj.transform.position = startPos;
        gameObj.transform.localScale = Vector3.one * startScale;

        gameObj.transform.DOScale(endScale, duration);
        gameObj.transform.DOMove(endPos, duration).SetEase(moveEase).OnComplete(() => {
            pool.Return(gameObj);
            completed.Invoke();
        });

        await UniTask.Delay((int)(duration * 1000));
        Complete();
        IsFlying = false;
    }

    public virtual void FlyBehaviour(GameObject gameObject) {
    }

    public virtual void Complete() {

    }
}
