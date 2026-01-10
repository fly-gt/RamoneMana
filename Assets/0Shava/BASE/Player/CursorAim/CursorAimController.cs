using UnityEngine;

public class CursorAimController : MonoBehaviour {
    public CursorAimModel model;
    public CursorAimView view;
    public Vector3 AimPoint => model.AimPoint;
    public Vector3 Direction => model.Direction;

    private void Awake() {
        model = new();
        view = GetComponent<CursorAimView>();
    }

    void Update() {
        if (!model.Active) {
            view.SetAimActive(false);
            return;
        }

        if (view.GetPoints(out var screenPos, out var aimPoint)) {
            model.AimPoint = aimPoint;
            model.Direction = (aimPoint - transform.position).SetY(0);
            view.SetAimPosition(screenPos);
            view.SetAimActive(true);
        } else {
            model.AimPoint = default;
            view.SetAimActive(false);
        }
    }

    public void SetActive(bool active) {
        model.Active = active;
    }
}

public class CursorAimModel {
    public Vector3 AimPoint;
    public Vector3 Direction;
    public bool Active = true;
}
