using UnityEngine;

public static class CameraUtility {
    //in camera field of view
    public static bool IsInView(Camera camera, Vector3 worldPosition) {
        Vector3 viewportPoint = camera.WorldToViewportPoint(worldPosition);

        bool isVisible = viewportPoint.z > 0 &&
                         viewportPoint.x >= 0 && viewportPoint.x <= 1 &&
                         viewportPoint.y >= 0 && viewportPoint.y <= 1;

        return isVisible;
    }

    public static Vector2 WorldToScreen_LocalPoint(Camera camera, RectTransform rectTransform,  Vector3 worldPoint) {
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(camera, worldPoint);
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPoint, null, out localPoint);
        return localPoint;
    }
}
