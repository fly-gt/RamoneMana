using System;

[Serializable]
public class PlayerModel {
    public PlayerStats Stats;

    public PlayerModel(PlayerStats stats) {
        Stats = stats;
    }
}

[Serializable]
public class PlayerStats {
    public float RotateSpeed = 5f;
}