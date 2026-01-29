using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour {
    public TutorialStats stats;
    public TutorialModel model;
    public TutorialView view;
    private List<TutorialStepBase> steps;
    [Space]
    public TutorialStepBase current;
    public int Step => model.Iterator;

    private void Awake() {
        model = new(stats);
        view = GetComponent<TutorialView>();

        steps = new() {
            //new EnemyClick(view)
        };
    }

    public async void Begin() {
        //GameController.Instance.SetComponents_Tutorial(false);

        gameObject.SetActive(true);

        await UniTask.Delay(1000);

        if (model.Iterator == 0) {
            //ScreenManager.Instance.Set<GameScreen>();
        }

        while (model.Iterator < steps.Count) {
            current = steps[model.Iterator];
            await current.Use();
            model.IncreaseIterator();
        }

        //ProfileController.Instance.SetTutorial(false);
        //GameController.Instance.SetComponents_Tutorial(true);
        //GameController.Instance.StartGameplay(false);
        Disable();
    }

    public void Disable() {
        gameObject.SetActive(false);
    }
}

public class TutorialModel {
    public TutorialStats Stats;
    public int Iterator;

    public TutorialModel(TutorialStats stats) {
        Stats = stats;
    }

    public void IncreaseIterator() {
        Iterator++;
    }
}

[Serializable]
public class TutorialStats {
    public float CanvasGroupFade = 0.5f;
}
