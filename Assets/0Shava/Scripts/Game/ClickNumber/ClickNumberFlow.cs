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

        failure = new(board, score);
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
        } else {
            VibrationManager.Medium();
            AudioManager.TryPlay(AEShared.asset.clickNumber, new AudioPlayData {
                Position = Camera.main.transform.position,
            });
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
    public ScoreController score;

    public FailureFacade(BoardController board, ScoreController score) {
        this.board = board;
        this.score = score;
    }

    public async void Failure() {
        Debug.Log("Failure");
        VibrationManager.Failure();
        AudioManager.TryPlay(AEShared.asset.failureNumber, new AudioPlayData {
            Position = Camera.main.transform.position,
        });
        ClickManager.Instance.blocked = true;
        await ScreenManager.Instance.Get<GameScreen>().WrongEffect();
        var substractScore = Mathf.Clamp(score.Score, 0, 50);

        if (substractScore != 0) {
            score.SubtractScore(substractScore);
            ScreenManager.Instance.Get<GameScreen>().ScoreTextFly(substractScore, false);
        }

        await UniTask.Delay(200);
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
        Debug.Log("Success1");

        VibrationManager.Success();
        Debug.Log("Success2");

        AudioManager.TryPlay(AEShared.asset.successNumber, new AudioPlayData {
            Position = Camera.main.transform.position,
        });
        Debug.Log("Success3");

        await Fly();
        Debug.Log("Success4");

        await UniTask.Delay(1000);
        Debug.Log("Success5");

        progress.Generate();
        Debug.Log("Success6");

        board.ResetupClicked();
        Debug.Log("Success7");

        board.UnClickNumbers();
        Debug.Log("Success8");

        ClickManager.Instance.blocked = false;
        Debug.Log("Success9");

    }

    private async UniTask Fly() {
        Debug.Log("Fly 1");

        var scoreRect = score.view.GetComponent<RectTransform>();
        Debug.Log("Fly 2");

        var totalScore = 0;

        foreach (var n in board.clickedNumbers) {
            Debug.Log("Fly 2 1");

            var isLast = n == board.clickedNumbers[^1];
            Debug.Log("Fly 2 2");

            var value = n.Number * Random.Range(6, 8);
            Debug.Log("Fly 2 3");

            totalScore += value;
            Debug.Log($"Fly 2 4 {FlyShared.Instance != null} {FlyShared.Instance?.StarFly != null}");

            FlyShared.Instance.StarFly.Fly(n.transform.position, scoreRect.position, () => onCompleted(value, isLast));
            Debug.Log("Fly 2 5");

            await UniTask.Delay(100);
            Debug.Log("Fly 2 6");

        }
        Debug.Log("Fly 3");

        void onCompleted(int value, bool last) {
            VibrationManager.Medium();
            AudioManager.TryPlay(AEShared.asset.addScore, new AudioPlayData {
                Position = Camera.main.transform.position,
            });
            score.AddScore(value, pulse: true);

            if (last) {
                ScreenManager.Instance.Get<GameScreen>().ScoreTextFly(totalScore, true);
            }
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
