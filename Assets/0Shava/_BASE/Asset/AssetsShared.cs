public class AssetsShared : Singletone<AssetsShared> {


    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }
}
