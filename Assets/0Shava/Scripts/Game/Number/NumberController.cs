using DG.Tweening;
using System;
using UnityEngine;

[RequireComponent (typeof(NumberView))]
public class NumberController : MonoBehaviour, IClickable {
    public NumberData data;
    public NumberView view;
    [Space]
    public float clickedHoldTime = 3f;
    public float timer;
    public bool clickedHold;

    private void Awake() {
        data = new();
        view = GetComponent<NumberView>();
        data.ChangeNumber += OnChangeNumber;
    }

    private void OnDestroy() {
        data.ChangeNumber -= OnChangeNumber;
    }

    private void Update() {
        if (Time.time > timer) {
            if (clickedHold) {
                clickedHold = false;
                transform.DOScale(Vector3.one, 0.2f);
                Debug.Log("unhold");
            }
        }
    }

    public void Setup(float x, float y) {
        data.SetNumber(UnityEngine.Random.Range(1, 10));
        view.SetSize(x, y);
    }

    public void SetPosition(Vector3 pos) {
        transform.localPosition = pos;
    }

    public void OnClick() {
        if (clickedHold) {
            return;
        }

        clickedHold = true;
        timer = Time.time + clickedHoldTime;
        transform.DOScale(Vector3.one * 0.85f, 0.2f);
        Debug.Log($"Click: {data.Number}");
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
