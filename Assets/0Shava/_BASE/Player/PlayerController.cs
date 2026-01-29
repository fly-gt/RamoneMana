using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public PlayerStats stats;
    public PlayerModel playerModel;
    public PlayerView playerView;
    //[Space]
    //public CursorAimController cursorAim;
    //public ColorArrowController colorArrow;
    //public ShotController shot;
    //public AbilityController ability;

    //private void Awake() {
    //    playerModel = new(stats);
    //    playerView = GetComponent<PlayerView>();
    //}

    //private void Update() {
    //    if (cursorAim.Direction != Vector3.zero) {
    //        playerView.SetRotate(cursorAim.Direction, stats.RotateSpeed);
    //    }
    //}

    //public void SetColorArrow(ColorType color) {
    //    colorArrow.SetColor(color);
    //}

    //public void ImproveAbility(Type type) {
    //    ability.ImproveAbility(type);
    //}

    //public void SetAttackSpeedMultiplier(float multiplier) {
    //    shot.SetAttackSpeedMultiplier(multiplier);
    //}
}

public class GameplayModel {
    public int PlayerPoints;
}
