public class AssetsShared : Singletone<AssetsShared> {
    public GameSettings Game;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }
}
