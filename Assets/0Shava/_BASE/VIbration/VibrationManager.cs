using UnityEngine;

public class VibrationManager : Singletone<VibrationManager> {
    public bool Enable;
    private IVibrationService vibrationService;

    private void Awake() {
        vibrationService = new TapticVibration();
    }

    public static void Medium() {
        if (!CanDo()) {
            return;
        }

        Instance.vibrationService.Medium();
    }

    public static void Success() {
        if (!CanDo()) {
            return;
        }

        Instance.vibrationService.Success();
    }

    public static void Failure() {
        if (!CanDo()) {
            return;
        }

        Instance.vibrationService.Failure();
    }

    private static bool CanDo() {
        return Instance && Instance.Enable;
    }
}

public interface IVibrationService {
    void Medium();
    void Success();
    void Failure();
}