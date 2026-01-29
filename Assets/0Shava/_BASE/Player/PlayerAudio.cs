using UnityEngine;

public class PlayerAudio : MonoBehaviour {
    public AnimatorCatchEvent animatorCatchEvent;
    public AudioEventAsset footstepAE;
    public AudioEventAsset loseAE;
    public Transform movable;
    private string footstepName = "Footstep";
    private int frame;

    private void Awake() {
        animatorCatchEvent.OnCatchEvent += OnCatchEvent;
    }

    private void OnDestroy() {
        animatorCatchEvent.OnCatchEvent -= OnCatchEvent;
    }

    private void OnCatchEvent(string str) {
        if (str == footstepName) {
            //if (PlayerView.Instance.GetAbility<PlayerMovementAbility>().moving) {
            //    if (frame == Time.frameCount) {
            //        return;
            //    }
            //    AudioManager.Instance.TryPlay(footstepAE, movable.position);
            //    frame = Time.frameCount;
            //}
        }
    }

    public void PlayerLose() {
        if (loseAE) {
            //AudioManager.TryPlay(loseAE, movable.position);
        }
    }
}
