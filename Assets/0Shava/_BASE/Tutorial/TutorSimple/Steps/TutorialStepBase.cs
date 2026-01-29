using Cysharp.Threading.Tasks;

public abstract class TutorialStepBase {
    protected TutorialView view;
    public TutorialStepBase(TutorialView v) {
        view = v;
    }

    public virtual UniTask Use() {
        return UniTask.CompletedTask;
    }
}
