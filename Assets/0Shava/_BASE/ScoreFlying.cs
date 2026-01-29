using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public class ScoreFlying : Singletone<ScoreFlying> {
    public ObjectPool pool;
    public float duration = 0.6f;
    public float startScale = 1f;
    public float endScale = 0.2f;
    public Ease moveEase = Ease.InOutQuad;

    public HashSet<GameObject> flying = new();

    public bool HasFlying => flying.Count > 0;

    public async void Fly(Vector3 startPos, Vector3 endPos, Action completed = null) {
        //var gameObj = Instantiate(prefab, startPos, prefab.transform.rotation);
        var gameObj = await pool.Get();
        flying.Add(gameObj);
        gameObj.transform.position = startPos;
        gameObj.transform.localScale = Vector3.one * startScale;

        gameObj.transform.DOMove(endPos, duration).SetEase(moveEase);
        gameObj.transform.DOScale(endScale, duration).SetEase(Ease.OutQuad).OnComplete(() => {
            //Destroy(gameObj); 
            flying.Remove(gameObj);
            pool.Return(gameObj);
            completed?.Invoke();
        });
    }
}
