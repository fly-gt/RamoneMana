using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {
    public SpriteRenderer numbersFieldSprite;
    public ObjectPool pool;
    public Vector2Int fieldSize;
    public float moveDuration = 0.5f;
    public float hideDuration = 0.2f;
    public List<NumberController> numbers = new();
    public List<NumberController> clickedNumbers = new();
    public BoardUnClickHandler unClickHandler;

    public event Action UnClick;

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

    public void UnClickNumbers() {
        foreach (var n in numbers) {
            n.UnClick();
        }

        clickedNumbers.Clear();
        UnClick?.Invoke();
    }

    public bool TryClickNumber(NumberController nc) {
        //проверяем соседей
        if (clickedNumbers.Count > 0 && !clickedNumbers[^1].neighboues.Contains(nc.Index)) {
            Debug.Log("R 2");
            return false;
        }

        //првоерка на clicked
        if (!nc.TryClick()) {
            Debug.Log("R 1");
            return false;
        }

        unClickHandler.Click();
        clickedNumbers.Add(nc);

        if (clickedNumbers.Count > 1) {
            GameController.Instance.boardLine.DrawLine(clickedNumbers[^2].transform.position, clickedNumbers[^1].transform.position);
        }

        return true;
    }

    public async UniTask Show() {
        Vector2 numberFieldSize = numbersFieldSprite.size;
        float itemSizeX = numberFieldSize.x / fieldSize.x;
        Vector2 itemSize = new Vector2(itemSizeX, itemSizeX);
        Vector3 pivotPosition = new(-numberFieldSize.x / 2, numberFieldSize.y / 2, 0);

        if (numbers.Count == 0) {
            for (int y = 0; y < fieldSize.y; y++) {
                for (int x = 0; x < fieldSize.x; x++) {
                    float nX = pivotPosition.x + x * itemSize.x + itemSize.x / 2;
                    float nY = pivotPosition.y - y * itemSize.y - itemSize.y / 2;
                    int index = x + y * fieldSize.x;

                    GameObject go = await pool.Get();
                    NumberController number = go.GetComponent<NumberController>();
                    number.Setup(itemSize.x, itemSize.y, index, fieldSize.x, fieldSize.y);
                    number.SetPosition(pivotPosition.SetVectorX(nX).SetVectorY(nY));

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
