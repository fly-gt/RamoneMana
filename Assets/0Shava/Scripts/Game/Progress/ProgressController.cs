using System;
using UnityEngine;

[RequireComponent(typeof(ProgressView))]
public class ProgressController : MonoBehaviour {
    public ProgressData data;
    public ProgressView view;

    public int Target => data.Target;
    public int Progress => data.Progress;

    private void Awake() {
        data = new();
        view = GetComponent<ProgressView>();

        data.ChangeTarget += OnChangeTarget;
        data.ChangeProgress += OnChangeProgress;
    }

    private void OnDestroy() {
        data.ChangeTarget -= OnChangeTarget;
        data.ChangeProgress -= OnChangeProgress;
    }

    public void Initialize(Vector2 sizeCamera) {
        transform.SetZ(1);
        transform.localScale = new Vector3(sizeCamera.x, sizeCamera.x, 1f);
    }

    public void Generate() {
        data.Generate();
    }

    public void AddProgress(int value) {
        data.AddProgress(value);
    }

    public void ClearProgress() {
        data.ClearProgress();
    }

    public void HideVisual() {
        view.Hide();
    }

    private void OnChangeTarget(int target) {
        view.SetTarget(target);
    }

    private void OnChangeProgress(int progress) {
        view.SetProgress(progress);
    }
}

[Serializable]
public class ProgressData {
    public int Target;
    public int Progress;

    public event Action<int> ChangeTarget;
    public event Action<int> ChangeProgress;

    public void AddProgress(int value) {
        Progress += value;
        ChangeProgress?.Invoke(Progress);
    }

    public void ClearProgress() {
        Progress = 0;
        ChangeProgress?.Invoke(Progress);
    }

    public void Generate() {
        Target = UnityEngine.Random.Range(10, 30);
        Progress = 0;
        ChangeProgress?.Invoke(Progress);
        ChangeTarget?.Invoke(Target);
    }
}
