using System;
using UnityEngine;

public class BoardUnClickHandler : MonoBehaviour {
    public float clickedHoldTime = 3f;
    public float timer;
    public bool clickedHold;
    public bool active;

    public event Action UnClick;

    private void Update() {
        if (!active) {
            return;
        }

        if (Time.time > timer) {
            if (clickedHold) {
                clickedHold = false;
                UnClick?.Invoke();
                Debug.Log("unhold");
            }
        }
    }

    public void Click() {
        timer = Time.time + clickedHoldTime;
        clickedHold = true;
    }

    public void Active(bool a) {
        active = a;

        if (!active) {
            timer = 0;
            clickedHold = false;
        }
    }
}
