using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {
    public SpriteRenderer numbersFieldSprite;
    public ObjectPool pool;
    public int fieldSize = 5;
    public float moveDuration = 0.5f;
    public float hideDuration = 0.2f;
    public List<NumberController> numbers = new();
    public BoardUnClickHandler unClickHandler;

    public event Action UnClick;

    //public int amount;

    private void Awake() {
        transform.SetY(transform.position.y - 15f);
        unClickHandler.UnClick += UnClickNumbers;
    }

    private void OnDestroy() {
        unClickHandler.UnClick -= UnClickNumbers;
    }

    public async UniTask Initialize(Vector2 sizeCamera) {
        transform.SetZ(1);
        transform.localScale = new Vector3(sizeCamera.x, sizeCamera.x, 1f);
    }

    public void UnclickActive(bool active) {
        unClickHandler.Active(active);
    }

    public void UnClickNumbers() {
        //amount = 0;
        foreach (var n in numbers) {
            n.UnClick();
        }
        UnClick?.Invoke();
    }

    public void ClickNumber(NumberController nc) {
        unClickHandler.Click();
        //amount += nc.Number;
    }

    public async UniTask Show() {
        Vector3 numberFieldSize = numbersFieldSprite.size;
        Vector2 itemSize = numberFieldSize / fieldSize;
        Vector3 pivotPosition = new(-numberFieldSize.x / 2, numberFieldSize.y / 2, 0);

        if (numbers.Count == 0) {
            for (int y = 0; y < fieldSize; y++) {
                for (int x = 0; x < fieldSize; x++) {
                    float nX = pivotPosition.x + x * itemSize.x + itemSize.x / 2;
                    float nY = pivotPosition.y - y * itemSize.y - itemSize.y / 2;

                    GameObject go = await pool.Get();
                    NumberController number = go.GetComponent<NumberController>();
                    number.Setup(itemSize.x, itemSize.y);
                    number.SetPosition(pivotPosition.SetX(nX).SetY(nY));

                    numbers.Add(number);
                }
            }
        }

        await transform.DOLocalMoveY(transform.position.y + 15f, moveDuration).SetEase(Ease.OutBack, 0.5f);
    }

    public async UniTask Hide() {
        await transform.DOLocalMoveY(transform.position.y - 15f, hideDuration);

        foreach (var n in numbers) {
            pool.Return(n.gameObject);
        }

        numbers.Clear();
    }
}
