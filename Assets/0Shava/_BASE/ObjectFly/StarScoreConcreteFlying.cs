using DG.Tweening;
using UnityEngine;

public class StarScoreConcreteFlying : ObjectFlying {
    public float startScale;
    public float endScale;
    public bool IsFlying;

    public override void FlyBehaviour(GameObject gameObject) {
        gameObject.transform.localScale = Vector3.one * startScale;
        gameObject.transform.DOScale(endScale, duration).SetEase(Ease.OutQuad);
        IsFlying = true;
    }

    public override void Complete() {
        IsFlying = false;
    }
}
