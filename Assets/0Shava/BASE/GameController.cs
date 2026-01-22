using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
//using YG;

public enum GameStateType {
    Menu,
    Game,
    Pause
}

public class GameController : Singletone<GameController> {
    public AssetReference enviromentPrefab;
    public AssetReference boardPrefab;
    public AssetReference progressPrefab;
    public AssetReference boardLinePrefab;
    [Space]
    public EnviromentController enviroment;
    public BoardController board;
    public ProgressController progress;
    public BoardLine boardLine;
    [Space]
    public ClickNumberFlow clickNumberFlow;
    [Space]
    public GameStateType State;

    private void OnDestroy() {
        clickNumberFlow?.Clear();
    }

    public async UniTask Initialize() {
        enviroment = await UtilityAdressables.InitializeObject<EnviromentController>(enviromentPrefab);
        Vector2 size = GetSizeByCamera();
        enviroment.Initialize(size);

        board = await UtilityAdressables.InitializeObject<BoardController>(boardPrefab);
        await board.Initialize(size);

        progress = await UtilityAdressables.InitializeObject<ProgressController>(progressPrefab);
        progress.Initialize(size);

        boardLine = await UtilityAdressables.InitializeObject<BoardLine>(boardLinePrefab);

        ToMenu(true);

        clickNumberFlow = new(progress, board, boardLine);
    }

    public async void ToMenu(bool first = false) {
        State = GameStateType.Menu;
        enviroment.ToMenu(first);
        progress.HideVisual();

        if (!first) {
            await board.Hide();
        }

        ScreenManager.Instance.Set<MenuScreen>();
    }

    public void ToPause() {
        State = GameStateType.Pause;
        PopupManager.Instance.Render<PausePopup>();
    }

    public async Task ToGameplay() {
        State = GameStateType.Game;
        ScreenManager.Instance.Set<GameScreen>();
        board.Show();
        enviroment.ToGame();
        progress.Generate();
    }

    private Vector2 GetSizeByCamera() {
        Vector2 size = UtilityCamera.CameraWorldSize(Camera.main);
        var spriteSize = enviroment.GetComponent<EnviromentController>().GetSpriteSize();
        var x = size.x / spriteSize.x;
        var y = size.y / spriteSize.y;

        return new Vector2(x, y);
    }
    #region old
    //public bool IsPlaying;
    //public CastleController castle;
    //public PlayerController player;
    //public ScoreController score;
    //public TutorialController tutorial;
    //public WaveController wave;
    //public GameStatistics gameStatistics;
    //[Space]
    //public AudioEventAsset levelSuccessAE;
    //public AudioEventAsset levelLoseAE;
    //public AudioEventAsset allLevelSuccessAE;
    //public bool isGameplaying;

    //private void Awake() {
    //    //gameStatistics = new();
    //}

    //private void Start() {
    //    GlobalKey.Instance.OnESC += ClickESC;
    //}

    //private void OnDestroy() {
    //    if (GlobalKey.Instance) {
    //        GlobalKey.Instance.OnESC -= ClickESC;
    //    }
    //}

    //public void Initialize() {
    //    isGameplaying = false;

    //    //PopupManager.Instance.CloseAll();
    //    if (ProfileController.Instance.GameEnded) {
    //        EndGame(true);
    //        return;
    //    }

    //    if (ProfileController.Instance.HasTutorial) {
    //        //tutorial.Begin();
    //        GameController.Instance.SetComponents_Tutorial(false);
    //        ScreenManager.Instance.Set<StartGameScreen>();
    //        return;
    //    } else {
    //        tutorial.Disable();
    //    }

    //    SetGameplayingActions(false);

    //    if (AppShared.Instance.isFirstGameplay) {
    //        ScreenManager.Instance.Set<StartGameScreen>();
    //    } else {
    //        StartGameplay();
    //        MusicManager.Instance.ResetMusic();
    //    }

    //    //YG2.MetricaSend("enter_game", new Dictionary<string, object> {
    //    //    { "level", PlayerController.Instance.model.Data.level},
    //    //    { "info", YG2.infoYG.name}
    //    //});
    //}

    //public async void StartGameplay(bool setGameScreen = true) {
    //    if (ProfileController.Instance.HasTutorial) {
    //        ScreenManager.Instance.Set<GameScreen>();
    //        tutorial.Begin();
    //        return;
    //    }

    //    isGameplaying = true;
    //    castle.Setup();
    //    wave.Setup();
    //    score.Setup();

    //    if (setGameScreen) {
    //        await ScreenManager.Instance.Set<GameScreen>();
    //        //player.colorArrow.SetColor(ColorType.Red);
    //    }

    //    Cursor.visible = false;
    //    SetGameplayingActions(true);
    //}

    //public void EnemyKilledByPlayer(Vector3 pos) {
    //    if (ProfileController.Instance.HasTutorial) {
    //        return;
    //    }

    //    score.EnemyKilled(pos);
    //    gameStatistics.EnemyKilled();
    //}

    //public void EnemyKilledByCastle(int damage) {
    //    if (ProfileController.Instance.HasTutorial) {
    //        return;
    //    }

    //    castle.TakeDamage(damage);
    //    gameStatistics.EnemyKilled();
    //}

    //public void EndWave() {
    //    gameStatistics.EndWave();
    //}

    //public async UniTask EndGame(bool win) {
    //    var isLast = ProfileController.Instance.Level == GameController.Instance.wave.asset.levelCount - 1;

    //    if (isLast) {
    //        ProfileController.Instance.EndGame();
    //        var newScore = ProfileController.Instance.ScoreGeneral + GameController.Instance.score.Score;
    //        ProfileController.Instance.SetScoreGeneral(newScore);

    //        if (ProfileController.Instance.ScoreGeneral > ProfileController.Instance.BestScore) {
    //            YG2.SetLeaderboard("leaderboardfaster", ProfileController.Instance.ScoreGeneral);
    //        }
    //    }

    //    foreach (var e in FindObjectsByType<EnemyController>(FindObjectsSortMode.None)) {
    //        e.Stop();
    //    }

    //    SetGameplayingActions(false);
    //    Cursor.visible = true;
    //    wave.End();

    //    await UniTask.Delay(300);
    //    MusicManager.Instance.Stop(0.5f);

    //    if (isLast) {
    //        //all level success
    //        AudioManager.Instance.TryPlay(allLevelSuccessAE, transform.position);
    //        PopupManager.Instance.Render<EndGamePopup>(new EndGamePopupCtx {
    //            score = ProfileController.Instance.ScoreGeneral
    //        });
    //    } else if (win) {
    //        //win level
    //        AudioManager.Instance.TryPlay(levelSuccessAE, transform.position);
    //        PopupManager.Instance.Render<SuccessPopup>();
    //    } else {
    //        //lose level
    //        AudioManager.Instance.TryPlay(levelLoseAE, transform.position);
    //        PopupManager.Instance.Render<LosePopup>();
    //    }
    //}

    //public void SetComponents_Tutorial(bool active) {
    //    score.SetActiveView(active);
    //    castle.SetActiveView(active);
    //    player.colorArrow.SetActiveView(active);
    //    player.ability.SetActiveView(active);

    //    SetGameplayingActions(active);
    //}

    //public void SetGameplayingActions(bool active) {
    //    player.colorArrow.SetActive(active);
    //    player.shot.SetActive(active);
    //    player.cursorAim.SetActive(active);
    //    wave.SetActiveView(active);
    //}

    //public bool isMenu;
    //public bool isProcessing;
    //public async void ClickESC() {
    //    var popups = PopupManager.Instance.Current && PopupManager.Instance.Current is not PausePopup;

    //    if (!isGameplaying || isProcessing || popups) {
    //        return;
    //    }

    //    isProcessing = true;

    //    if (!isMenu) {
    //        Time.timeScale = 0;
    //        GameController.Instance.player.cursorAim.SetActive(false);
    //        Cursor.visible = true;
    //        await PopupManager.Instance.Render<PausePopup>();
    //        isMenu = true;
    //    } else {
    //        await PopupManager.Instance.Close();
    //        Time.timeScale = 1;
    //        Cursor.visible = false;
    //        GameController.Instance.player.cursorAim.SetActive(true);
    //        isMenu = false;
    //    }

    //    isProcessing = false;
    //}

    //public void Clear() {
    //    PopupManager.Instance.CloseAll();
    //}

    #endregion
}
