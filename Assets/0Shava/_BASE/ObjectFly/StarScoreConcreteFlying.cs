using UnityEngine;

public class StarScoreConcreteFlying : ObjectFlying {
    public bool IsFlying;

    public override void FlyBehaviour(GameObject gameObject) {
        IsFlying = true;
    }

    public override void Complete() {
        IsFlying = false;
    }
}
