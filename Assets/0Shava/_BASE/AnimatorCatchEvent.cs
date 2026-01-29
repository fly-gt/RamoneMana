using System;
using UnityEngine;

public class AnimatorCatchEvent : MonoBehaviour {
    public Action<string> OnCatchEvent;
    public void CatchEvent(string str) {
        OnCatchEvent?.Invoke(str);
    }
}
