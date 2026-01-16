using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class EnviromentController : MonoBehaviour {
    public SpriteRenderer nightSprite;
    public SpriteRenderer grassSprite;
    public SpriteRenderer treesSprite;
    public float moveDuration = 0.5f;
    public float hideDuration = 0.2f;

    public void Initialize(Vector2 sizeCamera) {
        transform.SetZ(2);
        transform.localScale = new Vector3(sizeCamera.x, sizeCamera.y, 1f);
    }

    public void ToMenu(bool first = false) {
        var offsetPos = grassSprite.transform.localPosition.y - 15;

        if (first) {
            grassSprite.transform.SetY(offsetPos);
            //grassSprite.gameObject.SetActive(false);
            return;
        }

        grassSprite.transform.DOLocalMoveY(offsetPos, hideDuration);
    }

    public void ToGame() {
        var offsetPos = grassSprite.transform.localPosition.y + 15;
        grassSprite.transform.DOLocalMoveY(offsetPos, moveDuration).SetEase(Ease.OutBack, 0.5f);

        //grassSprite.gameObject.SetActive(true);
    }

    public Vector2 GetSpriteSize() {
        return nightSprite.size;
    }

    [Button]
    public void Size() {
        Vector2 size = UtilityCamera.CameraWorldSize(Camera.main);
        var spriteSize = GetSpriteSize();
        var x = size.x / spriteSize.x;
        var y = size.y / spriteSize.y;

        transform.localScale = new Vector3(x, y, 1f);
        FindFirstObjectByType<BoardController>().transform.localScale = new Vector3(x, x, 1f);
        FindFirstObjectByType<ProgressController>().transform.localScale = new Vector3(x, x, 1f);
    }
}
