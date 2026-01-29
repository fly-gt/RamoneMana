using UnityEngine;

public class TapticVibration : IVibrationService {
    public void Failure() {
        Taptic.Failure();
    }

    public void Medium() {
        Taptic.Medium();
    }

    public void Success() {
        Taptic.Success();
    }
}
