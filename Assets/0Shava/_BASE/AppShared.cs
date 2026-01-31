using UnityEngine;

public class AppShared : Singletone<AppShared> {
    public AppStateMachine appState;
    public GameSettings settings;
    public AppEventsService appEventsService;

    private void Awake() {
        appState = new AppStateMachine();
        DontDestroyOnLoad(gameObject);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
        }
    }
}
