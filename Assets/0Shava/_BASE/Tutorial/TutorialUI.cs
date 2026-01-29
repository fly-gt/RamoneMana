using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour {
    public TMP_Text messageText;
    public RectTransform message;
    public RectTransform mark;
    public RectTransform rectTransform;

    private void Awake() {
        messageText.text = string.Empty;
        message.gameObject.SetActive(false);
        mark.gameObject.SetActive(false);
    }
}
