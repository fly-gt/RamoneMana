using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MarkUi : MonoBehaviour, IPointerDownHandler {
    public event Action OnDown;
    public void OnPointerDown(PointerEventData eventData) {
        Debug.Log("Click");
        OnDown?.Invoke();
    }
}
