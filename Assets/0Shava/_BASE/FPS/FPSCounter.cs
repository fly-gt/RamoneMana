using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSCounter : Singletone<FPSCounter> {
    [SerializeField] private TMP_Text fpsText;
    [SerializeField] private int frameRange;
    //[SerializeField] private List<string> stringCahce;


    private int[] fpsBuffer;
    private int fpsBufferIndex;

    private int fps;

    private void Awake() {
        DontDestroyOnLoad(Instance.gameObject);
    }

    private void Update() {
        if (Time.frameCount % 10 != 0) {
            return;
        }
        //var t = 1;
        //for (int i = 1; i < 10000000; i++) {
        //    t *= i;
        //    t /= i;
        //}

        if (fpsBuffer == null || frameRange != fpsBuffer.Length) {
            Initialize();
        }

        UpdateBuffer();
        Calculate();
        //fpsText.text = NumberCache.Numbers[Mathf.Clamp(fps, 0, 99)];
        fpsText.SetText("{0}", Mathf.Clamp(fps, 0, 99));
    }

    private void Initialize() {
        if (frameRange <= 0) {
            frameRange = 1;
        }

        fpsBuffer = new int[frameRange];
        fpsBufferIndex = 0;
    }

    private void UpdateBuffer() {
        fpsBuffer[fpsBufferIndex++] = (int)(1f / Time.unscaledDeltaTime);

        if (fpsBufferIndex >= frameRange) {
            fpsBufferIndex = 0;
        }
    }

    private void Calculate() {
        int sum = 0;

        for (int i = 0; i < frameRange; i++) {
            sum += fpsBuffer[i];
        }

        fps = sum / frameRange;
    }
}