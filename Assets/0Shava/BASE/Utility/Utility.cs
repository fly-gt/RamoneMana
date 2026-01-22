using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;

public static partial class Utility {
    public static void QuickSort(RaycastHit[] array, int leftIndex, int rightIndex) {
        var i = leftIndex;
        var j = rightIndex;
        var pivot = array[(leftIndex + rightIndex) / 2].distance;

        while (i <= j) {
            while (array[i].distance < pivot) {
                i++;
            }

            while (array[j].distance > pivot) {
                j--;
            }
            if (i <= j) {
                var temp = array[i];
                array[i] = array[j];
                array[j] = temp;
                i++;
                j--;
            }
        }

        if (leftIndex < j)
            QuickSort(array, leftIndex, j);
        if (i < rightIndex)
            QuickSort(array, i, rightIndex);
    }

    public static bool TryGetComponentAndParent<T>(this GameObject gameObject, out T interact) where T : Component {
        if (gameObject.TryGetComponent(out T intAction)) {
            interact = intAction;
            return true;
        }

        var interactAction = gameObject.GetComponentInParent<T>();

        if (interactAction) {
            interact = interactAction;
            return true;
        }

        interact = null;
        return false;
    }

    private static int lastColor = -1;
    public static T GetRandomEnumValue<T>(int from = 0) {
        Array values = Enum.GetValues(typeof(T));
        int random = -1;

        do {
            random = UnityEngine.Random.Range(from, values.Length);

            if (lastColor != -1 && lastColor == random) {
                continue;
            }

            lastColor = random;
            break;
        }
        while (true);

        return (T)values.GetValue(random);
    }

    #region SET VECTOR
    public static Vector3 SetVectorX(this Vector3 vector, float x) {
        var v = vector;
        v.x = x;
        return v;
    }

    public static Vector3 SetVectorY(this Vector3 vector, float y) {
        var v = vector;
        v.y = y;
        return v;
    }

    public static Vector3 SetVectorZ(this Vector3 vector, float z) {
        var v = vector;
        v.z = z;
        return v;
    }
    #endregion

    #region SET TRANSFORM POSITION
    public static void SetX(this Transform t, float x) {
        t.position = t.position.SetVectorX(x);
    }

    public static void SetY(this Transform t, float y) {
        t.position = t.position.SetVectorY(y);
    }

    public static void SetZ(this Transform t, float z) {
        t.position = t.position.SetVectorZ(z);
    }
    #endregion

    public static Color SetAlpha(this Color color, float a) {
        var c = color;
        c.a = a;
        return c;
    }

    public static void SetAlpha(this UnityEngine.UI.Image image, float a) {
        image.color = image.color.SetAlpha(a);
    }


    public static void WorldToRect(Vector3 posWorld, RectTransform rect, out Vector2 anchorPosition) {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(posWorld);
        Vector2 canvasPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, screenPoint, null, out canvasPos);
        anchorPosition = canvasPos;
    }

    /// <summary>
    /// Переводит точку из одного Canvas в другой (в пространстве RectTransform).
    /// Работает независимо от режима Canvas (Overlay, Camera, World).
    /// </summary>
    /// <param name="sourceCanvas">Исходный Canvas</param>
    /// <param name="targetCanvas">Целевой Canvas</param>
    /// <param name="sourcePosition">Позиция точки в исходном Canvas (в локальных координатах RectTransform)</param>
    /// <returns>Позиция точки в целевом Canvas (в локальных координатах RectTransform)</returns>
    public static Vector2 ConvertPointBetweenCanvases(Canvas sourceCanvas, Canvas targetCanvas, Vector2 sourcePosition) {
        // 1. Преобразуем локальные координаты исходного канваса в экранные координаты
        RectTransform sourceRect = sourceCanvas.GetComponent<RectTransform>();
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(sourceCanvas.worldCamera, sourceRect.TransformPoint(sourcePosition));

        // 2. Преобразуем экранные координаты в локальные координаты целевого канваса
        RectTransform targetRect = targetCanvas.GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            targetRect,
            screenPoint,
            targetCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : targetCanvas.worldCamera,
            out Vector2 targetLocalPoint
        );

        return targetLocalPoint;
    }

    public static float delta = 0.001f; // для численного вычисления наклона
    public static AnimationCurve GenerateCurve(float xMin, float xMax, int points, Func<float, float> formula) {
        Keyframe[] keys = new Keyframe[points];
        for (int i = 0; i < points; i++) {
            float t = (float)i / (points - 1);
            float x = Mathf.Lerp(xMin, xMax, t);

            float y = formula(x);

            // 🔹 Численно вычисляем производную:
            float dydx = (formula(x + delta) - formula(x - delta)) / (2f * delta);

            keys[i] = new Keyframe(x, y, dydx, dydx);
        }

        return new AnimationCurve(keys);
    }

    public static void Shuffle<T>(List<T> list) {
        for (int i = list.Count - 1; i > 0; i--) {
            int j = UnityEngine.Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

    public enum AnchorType {
        Default, Top, TopRight, Right, BottomRight, Bottom, BottomLeft, Left, TopLeft
    }

    public static void SetAnchor(RectTransform rectTransform, AnchorType anchorType) {
        if (rectTransform == null)
            return;

        Vector2 anchor;

        switch (anchorType) {
            case AnchorType.Top:
                anchor = new Vector2(0.5f, 1f);
                break;

            case AnchorType.TopRight:
                anchor = new Vector2(1f, 1f);
                break;

            case AnchorType.Right:
                anchor = new Vector2(1f, 0.5f);
                break;

            case AnchorType.BottomRight:
                anchor = new Vector2(1f, 0f);
                break;

            case AnchorType.Bottom:
                anchor = new Vector2(0.5f, 0f);
                break;

            case AnchorType.BottomLeft:
                anchor = new Vector2(0f, 0f);
                break;

            case AnchorType.Left:
                anchor = new Vector2(0f, 0.5f);
                break;
            case AnchorType.TopLeft:
                anchor = new Vector2(0f, 1f);
                break;
            default:
                //default
                anchor = new Vector2(0.5f, 0.5f);
                break;
        }

        rectTransform.anchorMin = anchor;
        rectTransform.anchorMax = anchor;
        //rectTransform.pivot = anchor;
    }
}
