using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using YG;

public class SuccessPopup : BasePopup {
    //public CanvasGroup canvasGroup;
    //public Button nextButton;
    //public AbilitySelectUI[] abilities;
    //public RectTransform abilityTutorRect;
    //[Space]
    //public AbilitySelectUI selectedAbility;

    //public List<AbilityBase> improveAbilities = new();

    //public UniTaskCompletionSource nextCompletionSource;
    //private SuccessPopupContext successPopupContext;

    //public event Action OnNext;
    //public event Action OnSelectAbility;

    //public override async UniTask Render(object ctx) {
    //    if (ctx is SuccessPopupContext successCtx) {
    //        successPopupContext = successCtx;
    //    } else {
    //        successPopupContext = default;
    //    }

    //    nextCompletionSource = new();

    //    foreach (var ab in abilities) {
    //        ab.UnSelect();
    //    }

    //    nextButton.interactable = false;
    //    GameController.Instance.player.ability.GetAbilitiesToImprove(improveAbilities);

    //    for (int i = 0; i < improveAbilities.Count; i++) {
    //        var localizeText = Localization.Instance.Get(improveAbilities[i].Data.Name);
    //        var text = $"{localizeText} {improveAbilities[i].GetImproveName()}";
    //        abilities[i].Setup(improveAbilities[i].Data.Icon, text, improveAbilities[i].Data.GetType());
    //    }

    //    if (successPopupContext.isTutorial) {
    //        abilities[1].button.interactable = false;
    //    } else {
    //        foreach (var a in abilities) {
    //            a.button.interactable = true;
    //        }
    //    }

    //    await base.Render(ctx);
    //}

    //public void Select(AbilitySelectUI a) {
    //    foreach (var ab in abilities) {
    //        ab.UnSelect();
    //    }

    //    a.Select();
    //    selectedAbility = a;
    //    nextButton.interactable = true;
    //    OnSelectAbility?.Invoke();
    //}

    //public void NextClick() {
    //    if (!successPopupContext.isTutorial) {
    //        var newScore = ProfileController.Instance.ScoreGeneral + GameController.Instance.score.Score;

    //        ProfileController.Instance.SetLevel(ProfileController.Instance.Level + 1);
    //        ProfileController.Instance.SetScoreGeneral(newScore);
    
    //        if (ProfileController.Instance.ScoreGeneral > ProfileController.Instance.BestScore) {
    //            YG2.SetLeaderboard("leaderboardfaster", ProfileController.Instance.ScoreGeneral);
    //        }

    //        YG2.InterstitialAdvShow();
    //        AppShared.Instance.appState.SetState<GameState>();
    //    }

    //    GameController.Instance.player.ImproveAbility(selectedAbility.type);
    //    nextCompletionSource.TrySetResult();
    //    PopupManager.Instance.Close();
    //}
}

public struct SuccessPopupContext {
    public bool isTutorial;
}
