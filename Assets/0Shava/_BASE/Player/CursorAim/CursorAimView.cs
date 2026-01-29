using UnityEngine;

public class CursorAimView : MonoBehaviour {
    public Camera cam;          // Ссылка на камеру (если не main)
    public LayerMask groundMask; // Маска земли
    public RectTransform aim;  // UI элемент прицела

    public void SetAimPosition(Vector3 screenPos) {
        aim.position = screenPos;
    }

    public void SetAimActive(bool active) {
        aim.gameObject.SetActive(active);
    }

    public bool GetPoints(out Vector3 screenPos, out Vector3 aimPoint) {
        screenPos = default;
        aimPoint = default;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        // Проверяем пересечение с землёй
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundMask)) {
            // Конвертируем мировую позицию в экранную
            screenPos = cam.WorldToScreenPoint(hit.point);
            aimPoint = hit.point;
            // направление до цели

            return true;
            //if (direction.magnitude < 0.1f) 
            //    return; // если очень близко — не вращаемся
        } 

        return false;
    }
}
