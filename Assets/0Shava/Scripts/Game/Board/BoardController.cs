using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class BoardController : MonoBehaviour {
    public SpriteRenderer boardSprite;
    public float moveDuration = 0.5f;

    private void Awake() {
        transform.SetY(transform.position.y - 15f);
    }

    public void Show() {
        transform.DOLocalMoveY(transform.position.y + 15f, moveDuration).SetEase(Ease.OutBack, 0.5f);
    }

    public async UniTask Hide() {
        await transform.DOLocalMoveY(transform.position.y - 15f, moveDuration).SetEase(Ease.InOutBack);
    }
}
