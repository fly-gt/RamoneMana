using UnityEngine;
//Anchors панели должны быть Stretch
//Render Mode: Screen Space - Overlay или Camera
//Canvas Scaler:
//UI Scale Mode: Scale With Screen Size
//Match: 0.5(или под проект)

[RequireComponent(typeof(RectTransform))]
public class SafeArea : MonoBehaviour {
    RectTransform rectTransform;
    Rect lastSafeArea;

    void Awake() {
        rectTransform = GetComponent<RectTransform>();
        ApplySafeArea();
    }

    void Update() {
        if (Screen.safeArea != lastSafeArea)
            ApplySafeArea();
    }

    void ApplySafeArea() {
        Rect safeArea = Screen.safeArea;
        lastSafeArea = safeArea;

        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
    }
}
