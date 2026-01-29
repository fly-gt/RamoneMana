public interface IState<out TContext> : IState
    where TContext : IContext {
}

public interface IContext {
}

public interface IContextable<in TContext>
    where TContext : IContext {

    void SetContext<T>(T context) where T : TContext;
}