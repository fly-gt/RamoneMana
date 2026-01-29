using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class LocalizationText : MonoBehaviour {
    public TMP_Text text;
    public string key;

    private void Awake() {
        text = GetComponent<TMP_Text>();
    }

    private void Start() {
        if (text && key != string.Empty) {
            text.text = Localization.Instance.Get(key);
        }
    }

#if UNITY_EDITOR
    [Button]
    public void Ru() {
        var locaData = Localization.GetData_DEBUG(key);
        if (locaData != null && text && key != string.Empty) {
            text.text = locaData.ru;
        }
    }

    [Button]
    public void En() {
        var locaData = Localization.GetData_DEBUG(key);
        if (locaData != null && text && key != string.Empty) {
            text.text = locaData.en;
        }
    }
#endif
}
