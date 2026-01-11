using NaughtyAttributes;
using System;
using UnityEngine;

public class EnviromentSize : MonoBehaviour {
    public SpriteRenderer spriteRenderer;

    public Vector2 GetSpriteSize() {
        return spriteRenderer.size;
    }

    [Button]
    public void Size() {
        Vector2 size = UtilityCamera.CameraWorldSize(Camera.main);
        var spriteSize = GetSpriteSize();
        var x = size.x / spriteSize.x;
        var y = size.y / spriteSize.y;

        transform.localScale = new Vector3(x, y, 1f);
        FindFirstObjectByType<BoardController>().transform.localScale = new Vector3(x, x, 1f);
    }
}
