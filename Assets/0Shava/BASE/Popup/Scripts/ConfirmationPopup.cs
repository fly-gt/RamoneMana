using Cysharp.Threading.Tasks;
using System;
using TMPro;

public class ConfirmationPopup : BasePopup {
    public event Action OnYes;
    public event Action OnNo;
    public TMP_Text messageText;

    public override async UniTask Render(object ctx = null) {
        if (ctx is ConfirmationPopupContext conformCtx) {
            messageText.text = conformCtx.Message;
        }

        await base.Render(ctx);
    }

    public void YesClick() {
        OnYes?.Invoke();
    }

    public void NoClick() {
        OnNo?.Invoke();
    }
}

public struct ConfirmationPopupContext {
    public string Message;
}
