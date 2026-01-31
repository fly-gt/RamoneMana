using Cysharp.Threading.Tasks;
using UnityEngine;

public class ClickNumberFlow {
    public FailureFacade failure;
    public SuccessFacade success;
    public UnclickFacade unclick;

    public ProgressController progress;
    public BoardController board;
    public ScoreController score;
    private IAudioService audioService;

    public ClickNumberFlow(ProgressController progress, BoardController board, ScoreController score) {
        this.progress = progress;
        this.board = board;
        this.score = score;

        board.UnClick += OnUnClick;

        failure = new(board, score);
        success = new(progress, board, score);
        unclick = new(progress);

        audioService = ServiceLocator.Get<IAudioService>();
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
            audioService.Play(AEShared.asset.clickNumber, new AudioPlayData {
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
    private IAudioService audioService;

    public FailureFacade(BoardController board, ScoreController score) {
        this.board = board;
        this.score = score;
        audioService = ServiceLocator.Get<IAudioService>();
    }

    public async void Failure() {
        Debug.Log("Failure");
        VibrationManager.Failure();

        audioService.Play(AEShared.asset.failureNumber, new AudioPlayData {
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
    private IAudioService audioService;

    public SuccessFacade(ProgressController progress, BoardController board, ScoreController score) {
        this.progress = progress;
        this.board = board;
        this.score = score;
        audioService = ServiceLocator.Get<IAudioService>();
    }

    public async void Success() {
        ClickManager.Instance.blocked = true;
        VibrationManager.Success();

        audioService.Play(AEShared.asset.successNumber, new AudioPlayData {
            Position = Camera.main.transform.position,
        });

        await Fly();
        await UniTask.Delay(1000);
        progress.Generate();
        board.ResetupClicked();
        board.UnClickNumbers();
        ClickManager.Instance.blocked = false;
    }

    private async UniTask Fly() {
        var scoreRect = score.view.GetComponent<RectTransform>();
        var totalScore = 0;

        foreach (var n in board.clickedNumbers) {
            var isLast = n == board.clickedNumbers[^1];
            var value = n.Number * Random.Range(6, 8);
            totalScore += value;
            FlyShared.Instance.StarFly.Fly(n.transform.position, scoreRect.position, () => onCompleted(value, isLast));
            await UniTask.Delay(100);
        }

        void onCompleted(int value, bool last) {
            VibrationManager.Medium();
            audioService.Play(AEShared.asset.addScore, new AudioPlayData {
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
