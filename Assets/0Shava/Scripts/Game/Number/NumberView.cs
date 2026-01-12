using Unity.VisualScripting;
using UnityEngine;

public class NumberView : MonoBehaviour {
    public SpriteRenderer numberSprite;
    public BoxCollider boxCollider;

    public void SetSize(float x, float y) {
        var sizePercent = 0.9f;
        numberSprite.size = new Vector2(x * sizePercent, y * sizePercent);
        boxCollider.size = new Vector3(x, y, boxCollider.size.z);
    }

    public void SetSprite(Sprite sprite) {
        numberSprite.sprite = sprite;
    }
}
