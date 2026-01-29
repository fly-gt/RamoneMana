using System;
using System.Collections.Generic;

public interface IMachinable<out TState>
    where TState : IState {
}

public interface IMachine<in TState>
    where TState : IState {

    void AddState<T>(T state)
        where T : IState;

    void SetState<T>()
        where T : IState;

    void Clear();
}

public class Machine<TState> : IMachinable<TState>, IMachine<TState>
    where TState : IState {

    private IState current;
    private Dictionary<Type, IState> states = new();

    public void AddState<T>(T state)
        where T : IState {
        states[typeof(T)] = state;
    }

    public void SetState<T>()
        where T : IState {

        current?.Clear();
        current = states[typeof(T)];
        current.Setup();
    }

    public void SetStateSimple<T>()
        where T : IState {
        current = states[typeof(T)];
    }

    public void Clear() {
        current?.Clear();
        current = null;
    }
}