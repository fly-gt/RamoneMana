using Cysharp.Threading.Tasks;
using UnityEngine;

public class ClickNumberFlow {
    public ProgressController progress;
    public BoardController board;

    public ClickNumberFlow(ProgressController progress, BoardController board) {
        this.progress = progress;
        this.board = board;

        progress.Failed += OnFailed;
        progress.Success += OnSuccess;
        board.UnClick += OnUnClick;
    }

    public void Clear() {
        progress.Failed -= OnFailed;
        progress.Success -= OnSuccess;
        board.UnClick -= OnUnClick;
    }

    public void ClickNumber(NumberController nc) {
        board.ClickNumber(nc);
        progress.AddProgress(nc.Number);
    }

    private async void OnFailed() {
        ClickManager.Instance.blocked = true;
        Debug.Log("Failed");
        await UniTask.Delay(2000);
        progress.ClearProgress();
        board.UnClickNumbers();
        ClickManager.Instance.blocked = false;
    }

    private async void OnSuccess() {
        ClickManager.Instance.blocked = true;
        Debug.Log("Success");
        await UniTask.Delay(2000);
        progress.Generate();
        board.UnClickNumbers();
        ClickManager.Instance.blocked = false;
    }

    private void OnUnClick() {
        progress.ClearProgress();
    }
}
