using System;
using System.Collections.Generic;

public interface IMachinable<out TState, out TContext>
    where TState : IState<TContext>
    where TContext : IContext {
}

public interface IMachine<TState, TContext>
    where TState : IState<TContext>
    where TContext : IContext {

    void AddState<T, Y>(T state)
        where T : IState<Y>
        where Y : IContext;

    void SetState<T, Y>(Y context)
        where T : IState<Y>
        where Y : IContext;

    void Clear();
}

public class Machine<TState, TContext> : IMachinable<TState, TContext>, IMachine<TState, TContext>
    where TState : IState<TContext>
    where TContext : IContext {

    private IState<TContext> current;
    private Dictionary<Type, IState<TContext>> states = new();

    public void AddState<T, Y>(T state)
        where T : IState<Y>
        where Y : IContext {

        states[typeof(T)] = state as IState<TContext>;
    }

    public void SetState<T, Y>(Y context)
        where T : IState<Y>
        where Y : IContext {

        current?.Clear();
        current = states[typeof(T)];

        if (context != null && current is IContextable<Y> contextable) {
            contextable.SetContext(context);
        }

        current.Setup();
    }

    public void Clear() {
        current?.Clear();
        current = null;
    }
}