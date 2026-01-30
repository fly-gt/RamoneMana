using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public class ObjectFlying : MonoBehaviour {
    public ObjectPool pool;
    public float duration = 0.6f;
    public Ease moveEase = Ease.InOutQuad;

    public async void Fly(Vector3 startPos, Vector3 endPos, Action completed = null) {
        GameObject gameObj = await pool.Get();

        FlyBehaviour(gameObj);

        gameObj.transform.position = startPos;
        gameObj.transform.DOMove(endPos, duration).SetEase(moveEase).OnComplete(() => {
            pool.Return(gameObj);
            completed.Invoke();
        });

        await UniTask.Delay((int)(duration * 1000));
        Complete();
    }

    public virtual void FlyBehaviour(GameObject gameObject) {
    }

    public virtual void Complete() {

    }
}
