using Cysharp.Threading.Tasks;
using UnityEngine;

public class ClickNumberFlow {
    public ProgressController progress;
    public BoardController board;
    public BoardLine boardLine;

    public ClickNumberFlow(ProgressController progress, BoardController board, BoardLine boardLine) {
        this.progress = progress;
        this.board = board;
        this.boardLine = boardLine;

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
        if (!board.TryClickNumber(nc)) {
            return;
        }

        progress.AddProgress(nc.Number);
        progress.CheckProgress();
    }

    private async void OnFailed() {
        ClickManager.Instance.blocked = true;
        Debug.Log("Failed");
        await ScreenManager.Instance.Get<GameScreen>().WrongEffect();
        await UniTask.Delay(200);
        //progress.ClearProgress();
        board.UnClickNumbers();

        ClickManager.Instance.blocked = false;
    }

    private async void OnSuccess() {
        ClickManager.Instance.blocked = true;
        Debug.Log("Success");
        await UniTask.Delay(1000);
        progress.Generate();
        board.UnClickNumbers();
        ClickManager.Instance.blocked = false;
    }

    private void OnUnClick() {
        progress.ClearProgress();
        BoardLine.Instance.Clear();
    }
}
