public class AppStateMachine : Machine<IAppState> {
    public AppStateMachine() {
        AddState(new MainState());
        AddState(new GameState());
    }
}

public interface IAppState : IState {
}

public class MainState : IAppState {
    public void Setup() {
        SceneTransfer.Transfer2(SceneName.Main);
    }

    public void Clear() {
    }
}

public class GameState : IAppState {
    public void Setup() {
        SceneTransfer.Transfer2(SceneName.Game);
    }

    public void Clear() {
    }
}

public class RootState : IAppState {
    public void Setup() {
        SceneTransfer.Transfer2(SceneName.Root);
    }

    public void Clear() {
    }
}
