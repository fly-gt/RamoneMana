using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(NumberView))]
public class NumberController : MonoBehaviour, IClickable {
    public NumberData data;
    public NumberView view;
    public bool clicked;
    public int Number => data.Number;
    public int Index => data.Index;
    
    public HashSet<int> neighboues = new();

    private void Awake() {
        data = new();
        view = GetComponent<NumberView>();
        data.ChangeNumber += OnChangeNumber;
    }

    private void OnDestroy() {
        data.ChangeNumber -= OnChangeNumber;
    }

    public void Setup(float x, float y, int index, int sizeX, int sizeY) {
        LightSetup();
        data.SetIndex(index);
        BoardUtility.FillNeighbors(sizeX, sizeY, index, neighboues);
        view.SetSize(x, y);
    }

    public void LightSetup() {
        data.SetNumber(UnityEngine.Random.Range(1, 10));
    }

    public void SetPosition(Vector3 pos) {
        transform.localPosition = pos;
    }

    public void UnClick(bool doScale = true) {
        clicked = false;

        if (doScale) {
            transform.DOScale(Vector3.one, 0.1f);
        } else {
            transform.localScale = Vector3.one;
        }
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

    public bool TryClick() {
        if (clicked) {
            return false;
        }

        clicked = true;
        transform.DOScale(Vector3.one * 0.85f, 0.2f);

        return true;
    }

    public void OnChangeNumber(int n) {
        view.SetSprite(AppShared.Instance.settings.numberData[data.Number - 1].Sprite);
    }

    [Serializable]
    public class NumberData {
        public int Number;
        public int Index;
        public event Action<int> ChangeNumber;

        public void SetNumber(int n) {
            Number = n;
            ChangeNumber?.Invoke(Number);
        }

        public void SetIndex(int i) {
            Index = i;
        }
    }
}
