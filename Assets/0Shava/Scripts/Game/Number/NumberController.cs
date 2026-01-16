using DG.Tweening;
using System;
using UnityEngine;

[RequireComponent (typeof(NumberView))]
public class NumberController : MonoBehaviour, IClickable {
    public NumberData data;
    public NumberView view;
    public bool clicked;
    public int Number => data.Number;

    private void Awake() {
        data = new();
        view = GetComponent<NumberView>();
        data.ChangeNumber += OnChangeNumber;
    }

    private void OnDestroy() {
        data.ChangeNumber -= OnChangeNumber;
    }

    public void Setup(float x, float y) {
        data.SetNumber(UnityEngine.Random.Range(1, 10));
        view.SetSize(x, y);
    }

    public void SetPosition(Vector3 pos) {
        transform.localPosition = pos;
    }

    public void UnClick() {
        clicked = false;
        transform.DOScale(Vector3.one, 0.1f);
    }

    public void OnClick() {
        if (clicked) {
            return;
        }

        //Debug.Log($"Click: {data.Number}");
        clicked = true;
        transform.DOScale(Vector3.one * 0.85f, 0.2f);
        GameController.Instance.clickNumberFlow.ClickNumber(this);
        //GameController.Instance.board.ClickNumber(this);
    }

    public void OnChangeNumber(int n) {
        view.SetSprite(AppShared.Instance.settings.numberData[data.Number - 1].Sprite);
    }

    [Serializable]
    public class NumberData {
        public int Number;
        public event Action<int> ChangeNumber;

        public void SetNumber(int n) {
            Number = n;
            ChangeNumber?.Invoke(Number);
        }
    }
}
