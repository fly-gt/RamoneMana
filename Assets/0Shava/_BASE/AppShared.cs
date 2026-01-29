using System;
using UnityEngine;

public class AppShared : Singletone<AppShared> {
    public AppStateMachine appState;
    public AddressablesCache addressables;
    public GameSettings settings;

    private void Awake() {
        appState = new AppStateMachine();
        DontDestroyOnLoad(gameObject);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
        }
    }
}
