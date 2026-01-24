using Cysharp.Threading.Tasks;
using UnityEngine;

public class ClickNumberFlow {
    public FailureFacade failure;
    public SuccessFacade success;
    public UnclickFacade unclick;

    public ProgressController progress;
    public BoardController board;
    public ScoreController score;

    public ClickNumberFlow(ProgressController progress, BoardController board, ScoreController score) {
        this.progress = progress;
        this.board = board;
        this.score = score;

        board.UnClick += OnUnClick;

        failure = new(board);
        success = new(progress, board, score);
        unclick = new(progress);
    }

    public void ClickNumber(NumberController nc) {
        if (!board.TryClickNumber(nc)) {
            return;
        }

        progress.AddProgress(nc.Number);

        if (progress.Progress == progress.Target) {
            success.Success();
        } else if (progress.Progress > progress.Target) {
            failure.Failure();
        }
    }

    private void OnUnClick() {
        unclick.Unclick();
    }

    public void Clear() {
        board.UnClick -= OnUnClick;
    }
}

public class FailureFacade {
    public BoardController board;

    public FailureFacade(BoardController board) {
        this.board = board;
    }

    public async void Failure() {
        Debug.Log("Failure");
        ClickManager.Instance.blocked = true;
        await ScreenManager.Instance.Get<GameScreen>().WrongEffect();
        await UniTask.Delay(200);
        //progress.ClearProgress();
        board.UnClickNumbers();

        ClickManager.Instance.blocked = false;
    }
}

public class SuccessFacade {
    public ProgressController progress;
    public BoardController board;
    public ScoreController score;

    public SuccessFacade(ProgressController progress, BoardController board, ScoreController score) {
        this.progress = progress;
        this.board = board;
        this.score = score;
    }

    public async void Success() {
        Debug.Log("Success");
        ClickManager.Instance.blocked = true;
        await Fly();
        await UniTask.Delay(1000);
        progress.Generate();
        //score.AddScore(board.clickedNumbers.Count * 10);
        board.UnClickNumbers();
        ClickManager.Instance.blocked = false;
    }

    private async UniTask Fly() {
        var flyRect = ScoreFlying.Instance.GetComponent<RectTransform>();
        var scoreRect = score.view.GetComponent<RectTransform>();

        foreach (var n in board.clickedNumbers) {
            ScoreFlying.Instance.Fly(n.transform.position, scoreRect.position, onCompleted);
            await UniTask.Delay(100);
        }

        void onCompleted() {
            score.AddScore(score: 10, pulse: true);
        }
    }
}

public class UnclickFacade {
    public ProgressController progress;

    public UnclickFacade(ProgressController progress) {
        this.progress = progress;
    }

    public void Unclick() {
        progress.ClearProgress();
        BoardLine.Instance.Clear();
    }
}
