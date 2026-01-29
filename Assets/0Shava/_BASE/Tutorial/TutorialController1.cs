using UnityEngine;
using System.Linq;

public class TutorialController1 : Singletone<TutorialController1> {
    public TutorialUI tutorialUI;
    public float markSpeed = 10000f;
    public float edgePadding = 30f; // отступ от края экрана
    public int tutorialCurrentStep;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    private void Update() {
        if (tutorialUI) {
            //TryDisplay();
        }
    }

    public void TryTurnOn() {
        //if (HasTutorial()) {
        //    LevelManager level = GameController.Instance.Level;
        //    currentStep = level.TutorialSteps.FirstOrDefault(x => x.step == tutorialCurrentStep);
        //    return;
        //}
    }

    //public bool HasTutorial() {
    //    return !PlayerController.Instance.playerModel.Data.TutorialSuccess;
    //}

    public bool TryCompleteStep(int step) {
        //if (tutorialCurrentStep == step) {
        //    tutorialCurrentStep++;

        //    //try set NEXT step
        //    currentStep = GameController.Instance.Level.TutorialSteps.FirstOrDefault(x => x.step == tutorialCurrentStep);
        //    TryDisplay(true);
        //    return true;
        //}

        return false;
    }

    //private void TryDisplay(bool forceUppd = false) { 
    //    if (currentStep == null) {
    //        SetMark(false);
    //        SetMessage(false, forceUppd);
    //    } else {
    //        SetMessage(true, forceUppd);
    //        SetMark(true);
    //        MarkCanvasPos(currentStep.transform.position);
    //    }
    //}

    //private void SetMark(bool active) {
    //    if (tutorialUI.mark.gameObject.activeSelf != active) {
    //        tutorialUI.mark.gameObject.SetActive(active);
    //    }
    //}

    //private void SetMessage(bool active, bool forceUppd = false) {
    //    if (forceUppd) {
    //        tutorialUI.message.gameObject.SetActive(active);
    //        tutorialUI.messageText.text = active ? currentStep.message : string.Empty;
    //        return;
    //    }

    //    if (tutorialUI.message.gameObject.activeSelf != active) {
    //        tutorialUI.message.gameObject.SetActive(active);
    //        tutorialUI.messageText.text = active ? currentStep.message : string.Empty;
    //    }
    //}

    private void MarkCanvasPos(Vector3 posWorld) {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(posWorld);
        bool isBehind = screenPoint.z < 0;

        // Если цель позади камеры — инвертируем координаты
        if (isBehind) {
            screenPoint *= -1;
        }

        Vector2 canvasPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(tutorialUI.rectTransform, screenPoint, null, out canvasPos);

        Vector2 clampedPosition = ClampToCanvas(canvasPos, tutorialUI.rectTransform);

        // Если цель в поле зрения
        if (!isBehind && screenPoint.x >= 0 && screenPoint.x <= Screen.width &&
            screenPoint.y >= 0 && screenPoint.y <= Screen.height) {
            tutorialUI.mark.anchoredPosition = canvasPos;
        } else {
            tutorialUI.mark.anchoredPosition = clampedPosition;
        }
    }

    // Ограничение позиции на границах канваса
    private Vector2 ClampToCanvas(Vector2 pos, RectTransform canvas) {
        Vector2 min = canvas.rect.min + Vector2.one * edgePadding;
        Vector2 max = canvas.rect.max - Vector2.one * edgePadding;

        return new Vector2(Mathf.Clamp(pos.x, min.x, max.x), Mathf.Clamp(pos.y, min.y, max.y));
    }
}
