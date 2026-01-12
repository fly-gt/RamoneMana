using UnityEngine;

public class ClickManager : MonoBehaviour {
    [SerializeField] private LayerMask clickableLayer;

    void Update() {
        // Мобильные
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
                TryClick(touch.position);
        }

        // Редактор / ПК
        if (Input.GetMouseButtonDown(0)) {
            TryClick(Input.mousePosition);
        }
    }

    void TryClick(Vector2 screenPos) {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10, clickableLayer)) {
            IClickable clickable = hit.collider.GetComponent<IClickable>();

            if (clickable != null) {
                clickable.OnClick();
            }
        }
    }
}

public interface IClickable {
    void OnClick();
}