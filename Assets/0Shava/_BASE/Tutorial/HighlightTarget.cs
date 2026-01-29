using UnityEngine;

public class HighlightPixelPerfect : MonoBehaviour {
    public Material highlightMaterial;
    public Transform target3D;
    public RectTransform targetUI;
    public Camera mainCamera;
    public float radius = 100f; // в пикселях

    void Update() {
        Vector2 screenPos = Vector2.zero;

        if (targetUI != null) {
            screenPos = RectTransformUtility.WorldToScreenPoint(null, targetUI.position);
        } else if (target3D != null) {
            screenPos = mainCamera.WorldToScreenPoint(target3D.position);
        }

        highlightMaterial.SetVector("_MaskCenter", new Vector4(screenPos.x, screenPos.y, 0, 0));
        highlightMaterial.SetFloat("_MaskRadius", radius);
    }
}
