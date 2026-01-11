using UnityEngine;

public class Number : MonoBehaviour {
    public SpriteRenderer numberSprite;
    public BoxCollider boxCollider;

    public void SetSize(float x, float y) { 
        numberSprite.size = new Vector2(x, y);
        boxCollider.size = new Vector3(x, y, boxCollider.size.z);
    }

    public void SetPosition(Vector3 pos) {
        transform.localPosition = pos;
    }
}
